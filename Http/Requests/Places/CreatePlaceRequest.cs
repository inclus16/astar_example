using InclusMap.Dto.Places;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Http.Requests.Places
{
    public class CreatePlaceRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double X { get; set; }

        public double Y { get; set; }

        public CreatePlaceDto GetDto()
        {
            return new CreatePlaceDto(Name, X, Y);
        }
    }
}
