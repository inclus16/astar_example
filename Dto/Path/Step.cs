using InclusMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Dto.Path
{
    public class Step
    {
        private readonly Place from;

        private Place to;

        private readonly double distance;

        public Place FromPlace { get => from; }

        public Place ToPlace { get => to; set => to = value; }

        public double Distance { get => distance; }

        public Step(Place from, Place to, double distance)
        {
            this.from = from;
            this.to = to;
            this.distance = distance;
        }

    }
}
