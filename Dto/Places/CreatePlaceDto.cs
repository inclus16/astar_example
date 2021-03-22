using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Dto.Places
{
    public class CreatePlaceDto
    {
        private readonly string name;

        private readonly double x;

        private readonly double y;

        public string Name { get => name; }

        public double X { get => x; }

        public double Y { get => y; }

        public CreatePlaceDto(string name, double x, double y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }
    }
}
