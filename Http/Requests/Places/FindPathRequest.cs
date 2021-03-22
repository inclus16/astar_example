using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Http.Requests.Places
{
    public class FindPathRequest
    {
        [Required]
        public int FromId { get; set; }

        [Required]
        public int ToId { get; set; }
    }
}
