using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace NetCoreWebApiBoilerPlate.Controllers
{
    public class ValuesController : ApiController
    {
        [System.Web.Http.Route("values/{page:int}")]
        [System.Web.Http.Route("values")]
        [ProducesResponseType(typeof(List<string>), 200)]
        public async Task<IHttpActionResult> Get([FromUri] QueryParams parameters, [FromRoute] int page = 1)
        {
            var parameterList = Request.GetQueryNameValuePairs();
            var modelValidation = ValidateModelRules(parameterList, out var invalidParameters, page);
            var values = new[] { "value4", "value1", "value2", "value6", "value3", "value9" };

            if (modelValidation != null)
            {
                return Content(HttpStatusCode.BadRequest, modelValidation);
            }

            if (parameters.Sort == "asc")
            {
                values = values.OrderBy(x => x).ToArray();
            }
            else
            {
                values = parameters.Sort == "desc" ? values = values.OrderByDescending(x => x).ToArray() : values;
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Add("X-TOTAL-COUNT", values.Length.ToString());
            response.Content = new StringContent(JsonConvert.SerializeObject(values.Skip((page - 1) * parameters.Count).Take(parameters.Count)));

            var resultado = values.Skip((page - 1) * parameters.Count).Take(parameters.Count);

            IHttpActionResult result = ResponseMessage(response);

            return result;
        }

        /// <summary>
        /// Validations & Rules
        /// </summary>
        /// <param name="parameterList"></param>
        /// <param name="invalidParameters"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private ErrorMessage ValidateModelRules(IEnumerable<KeyValuePair<string, string>> parameterList,
            out List<string> invalidParameters, int page)
        {
            bool isValid = true;
            var validKeys = new[] { "sort", "count" };
            invalidParameters = new List<string>();


            //validate parameter list
            foreach (var parameter in parameterList)
            {
                if (validKeys.All(q => q != parameter.Key))
                {
                    isValid = false;
                    invalidParameters.Add(parameter.Key);
                }
            }

            if (!isValid)
            {
                return new ErrorMessage
                {
                    Message = "Validation Failed",
                    InvalidFields = invalidParameters
                }; ;
            }

            var count = parameterList?.FirstOrDefault(q => string.Equals(q.Key.ToLower(), "count")).Value;
            if (Convert.ToInt32(count) < 0 || page < 1)
                return new ErrorMessage
                {
                    Message = "Parameter Invalid value",
                    InvalidFields = new List<string>
                    {
                        { "count" }, { "page" }
                    }
                };

            return null;
        }
    }

    /// <summary>
    /// Parameters
    /// </summary>
    public class QueryParams
    {
        public string Sort { get; set; } = "none";

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid count")]
        public int Count { get; set; } = 3;
    }

    /// <summary>
    /// Error media
    /// </summary>
    public class ErrorMessage
    {
        public string Message { get; set; }
        public List<string> InvalidFields { get; set; }
    }
}
