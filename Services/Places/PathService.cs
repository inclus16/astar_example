using InclusMap.Dto;
using InclusMap.Dto.Path;
using InclusMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Services.Places
{
    public class PathService
    {

        private readonly PlaceService placesService;

        private Place destinationPlace;

        private Place startPlace;

        private List<List<Step>> stepsHistory;

        private int currentStepHistory;

        public PathService(PlaceService places)
        {
            placesService = places;
        }

        public async Task<FindPathResult> FindShortestPath(int fromId, int toId)
        {
            List<Place> places = await placesService.GetPlaces();
            DateTime start = DateTime.Now;
            QueuePriority<Place> openPlaces = new QueuePriority<Place>();
            List<Place> visitedPlaces = new List<Place>();
            currentStepHistory = 0;
            stepsHistory = new List<List<Step>>
            {
                new List<Step>()
            };
            startPlace = places.First(x => x.Id == fromId);
            destinationPlace = places.First(x => x.Id == toId);
            double heureticTotal = GetDistance(startPlace.X, startPlace.Y, destinationPlace.X, destinationPlace.Y);
            Place current = startPlace;
            visitedPlaces.Add(startPlace);
            places.Where(x => current.Links.Select(x => x.ToId).Contains(x.Id)).ToList()
                .ForEach(x => openPlaces.Push(x, -(GetDistance(startPlace.X, startPlace.Y, x.X, x.Y) + FindHeuriticalToEnd(x))));
            while (!openPlaces.IsEmpty())
            {
                Place previouse = current;
                current = openPlaces.Take();
                    //If previose node has link to current
                if (previouse.Links.Select(x => x.ToId).Contains(current.Id))
                {  
                        stepsHistory[currentStepHistory].Add(new Step(previouse, current, GetDistance(previouse.X, previouse.Y, current.X, current.Y)));
                }
                else
                {
                    //If we 'change road' - trying to find existing step history that ways to current node
                    int lastStepHistoryWithLastStepBeforeChangeRoad = stepsHistory.FindIndex(x => x.Last().ToPlace.Links.Select(q=>q.ToId).Contains(current.Id));
                    if (lastStepHistoryWithLastStepBeforeChangeRoad > -1)
                    {
                        currentStepHistory = lastStepHistoryWithLastStepBeforeChangeRoad;
                    }
                    else
                    {
                        //If no existing step history was found - create new with last node that links to current
                        int lastStepIndexBeforeChangeRoad = stepsHistory[currentStepHistory].FindIndex(x => x.FromPlace.Links.Any(q => q.ToId == current.Id));
                        Step lastStep = stepsHistory[currentStepHistory][lastStepIndexBeforeChangeRoad];
                        stepsHistory.Add(stepsHistory[currentStepHistory].GetRange(0, stepsHistory.Count));
                        currentStepHistory++;
                    }                   
                    previouse = stepsHistory[currentStepHistory].Last().ToPlace;
                    stepsHistory[currentStepHistory].Add(new Step(previouse, current, GetDistance(previouse.X, previouse.Y, current.X, current.Y)));
                }
                //Add current place to visited
                if (!visitedPlaces.Contains(current))
                    visitedPlaces.Add(current);
                //Current place is destination
                if (current.Id == destinationPlace.Id)
                    break;
                //Add linked places to open if they: not already there, not in visited
                places.Where(x => current.Links.Select(x => x.ToId).Contains(x.Id)).Where(x => !openPlaces.HasItem(x)&&!visitedPlaces.Contains(x)).ToList()
                    .ForEach(x => openPlaces.Push(x, -(FindDistancePathFromStart(x) + FindHeuriticalToEnd(x))));
            }
            if (current.Id != destinationPlace.Id)
                throw new Exception("Path not found");
            List<Coordinates> coordinates = ConstructResult();
            stepsHistory.Reverse();
            return new FindPathResult(coordinates, DateTime.Now - start, stepsHistory);
        }

        private double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        private double FindHeuriticalToEnd(Place place)
        {
            return GetDistance(place.X, place.Y, destinationPlace.X, destinationPlace.Y);
        }

        private List<Coordinates> ConstructResult()
        {
            Place start = startPlace;
            List<Coordinates> coordinates = new List<Coordinates>();
            coordinates.Add(new Coordinates(start.X, start.Y));
            foreach (Step step in stepsHistory[currentStepHistory])
            {
                if (start.Id == step.FromPlace.Id)
                {
                    coordinates.Add(new Coordinates(step.ToPlace.X, step.ToPlace.Y));
                    if (step.ToPlace.Id == destinationPlace.Id)
                        break;
                    start = step.ToPlace;
                }
            }
            return coordinates;
        }

        private double FindDistancePathFromStart(Place place)
        {
            double totalDistance = 0;
            Place start = startPlace;
            foreach(Step step in stepsHistory[currentStepHistory])
            {
                if(start.Id == step.FromPlace.Id)
                {
                    totalDistance += step.Distance;
                    start = step.ToPlace;
                }
            }
            totalDistance += GetDistance(start.X, start.Y, place.X, place.Y);
            return totalDistance;
        }
    }
}
