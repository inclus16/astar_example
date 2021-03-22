using InclusMap.Http.Requests.Places;
using InclusMap.Models;
using InclusMap.Services.Places;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly PlaceService places;

        public PlaceController(PlaceService places)
        {
            this.places = places;
        }


        [HttpGet]
        public async Task<List<Place>> Get()
        {
            return await places.GetPlaces();
        }

        [HttpPost]
        public async Task Post(CreatePlaceRequest request)
        {
            await places.Create(request.GetDto());
        }

        [HttpPatch]
        public async Task Patch(AttackLinkRequest request)
        {
            await places.AttachLink(request.FromId, request.ToId);
        }
    }
}
