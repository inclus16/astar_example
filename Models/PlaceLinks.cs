using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Models
{
    public class PlaceLinks
    {
        public int FromId { get; set; }

        public int ToId { get; set; }

        public double Distance { get; set; }
    }
}
