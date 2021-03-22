using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Dto.Path
{
    public class Coordinates
    {
        private readonly double x;

        private readonly double y;

        public double X { get => x; }

        public double Y { get => y; }

        public Coordinates(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
