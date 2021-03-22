using InclusMap.Dto.Path;
using InclusMap.Http.Requests.Places;
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
    public class PathController : ControllerBase
    {
        private readonly PathService paths;

        public PathController(PathService paths)
        {
            this.paths = paths;
        }


        [HttpGet]
        public async Task<FindPathResult> Get([FromQuery]FindPathRequest request)
        {
           return await paths.FindShortestPath(request.FromId, request.ToId);
        }
    }
}
