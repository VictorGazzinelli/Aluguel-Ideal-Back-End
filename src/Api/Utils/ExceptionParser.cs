using AluguelIdeal.Api.Utils.Exceptions;
using AluguelIdeal.Api.Utils.Extensions;
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
                throw new ArgumentNullException(nameof(exception), "Can't parse null exception as http response");

            int statusCode = exception is HttpResponseException httpResponseException ?
                    httpResponseException.Status :
                    StatusCodes.Status500InternalServerError;
            Dictionary<string, object> responseBody = new Dictionary<string, object>();
            if (environment.IsDevelopment())
            {
                responseBody.Add(nameof(exception.Message).ToCamelCase(), exception.Message);
                responseBody.Add(nameof(Type).ToCamelCase(), exception.GetType().FullName);
                responseBody.Add(nameof(exception.StackTrace).ToCamelCase(), exception.StackTrace);
            }
            else
            {
                responseBody.Add(nameof(exception.Message).ToCamelCase(), exception is HttpResponseException ?
                    exception.Message :
                    "an error occured");
            }

            return (statusCode, responseBody);
        }
    }
}
