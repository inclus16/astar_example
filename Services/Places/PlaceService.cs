using InclusMap.Dto.Places;
using InclusMap.Models;
using InclusMap.Services.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Services.Places
{
    public class PlaceService
    {
        private readonly DatabaseContext db;

        public PlaceService(DatabaseContext db)
        {
            this.db = db;
        }

        public Task<List<Place>> GetPlaces()
        {
          return  db.Places.Include(q => q.Links).ToListAsync();
        }

        public Task Create(CreatePlaceDto dto)
        {
            db.Places.Add(new Place
            {
                Name = dto.Name,
                X = dto.X,
                Y = dto.Y,
            });
            return db.SaveChangesAsync();
        }

        public async Task AttachLink(int fromId, int toId)
        {
            Place from = await db.Places.FirstOrDefaultAsync(x => x.Id == fromId);
            Place to = await db.Places.FirstOrDefaultAsync(x => x.Id == toId);
            double distance = GetDistance(from.X, from.Y, to.X, to.Y);
            from.Links.Add(new PlaceLinks
            {
                FromId = fromId,
                ToId = toId,
                Distance = distance
            });
            to.Links.Add(new PlaceLinks
            {
                FromId = toId,
                ToId = fromId,
                Distance = distance
            });
            db.UpdateRange(new[] { from, to });
            await db.SaveChangesAsync();
        }

        private double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

    }
}
