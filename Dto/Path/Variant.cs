using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Dto.Path
{
    public class Variant
    {
        private readonly List<Step> steps;

        public string Path { get => string.Join("->", steps.SelectMany(x => new[] { x.FromPlace.Name, x.ToPlace.Name }).Distinct()); }

        public Variant(List<Step> steps)
        {
            this.steps = steps;
          
        }
    }
}
