using AluguelIdeal.Api.Utils.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace AluguelIdeal.Api.Utils
{
    public class ExceptionParser
    {
        private readonly IHostEnvironment environment;

        public ExceptionParser(IHostEnvironment environment)
        {
            this.environment = environment;
        }

        public (int statusCode, Dictionary<string, object> responseBody) AsHttpResponse(Exception exception)
        {
            if (exception == null)
                throw new Exception("Can't parse null exception as http response");

            Dictionary<string, object> responseBody = new Dictionary<string, object>()
            {
                ["message"] = exception.Message,
            };
            if (environment.IsDevelopment())
            {
                responseBody.Add("type", exception.GetType().FullName);
                responseBody.Add("stackTrace", exception.StackTrace);
            }

            int statusCode = exception is HttpResponseException httpResponseException ?
                    httpResponseException.Status :
                    StatusCodes.Status500InternalServerError;
            return (statusCode, responseBody);
        }
    }
}
