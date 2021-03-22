using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Dto.Path
{
    public class FindPathResult
    {
        private readonly TimeSpan time;

        private readonly List<Coordinates> path;

        private readonly List<Variant> variants;

        public double Time { get => time.TotalMilliseconds; }

        public List<Coordinates> Path { get => path; }

        public List<string> Variants { get => variants.Select(x => x.Path).ToList(); }


        public FindPathResult(List<Coordinates> path,TimeSpan time,List<List<Step>> stepHistory)
        {
            this.time = time;
            this.path = path;
            variants = stepHistory.Select(x => new Variant(x)).ToList();
        }
    }
}
