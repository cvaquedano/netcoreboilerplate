using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWebApiBoilerPlate.Controllers
{
    public class RequestDto
    {

        [FromRoute]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid count")]
        public int Id { get; set; } = 1;

        [FromBody]
        public List<string> Collection { get; set; }


        [FromQuery]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid count")]        
        public int Count { get; set; } = 3;

        [FromQuery]
        public string OrderBy { get; set; } = "None";

    }
}
