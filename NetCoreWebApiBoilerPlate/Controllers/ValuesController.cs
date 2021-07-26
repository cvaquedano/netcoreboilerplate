using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost("", Name = "values")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<string>>> Get( RequestDto requestDto)
        {
            var colleccion = new List<string>();

             Response.Headers.Add("X-Total-Count", requestDto.Collection.Count.ToString());
            return Ok(colleccion);
        }



    }
}
