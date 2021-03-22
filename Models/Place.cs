using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Models
{
    [Index(nameof(X))]
    [Index(nameof(Y))]
    public class Place
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public List<PlaceLinks> Links { get; set; } = new List<PlaceLinks>();
    }
}
