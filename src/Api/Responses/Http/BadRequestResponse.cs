using System.Collections.Generic;

namespace AluguelIdeal.Api.Controllers.Models.Responses.Http
{
    public class BadRequestResponse
    {
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
