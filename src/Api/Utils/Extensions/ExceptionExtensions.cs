using AluguelIdeal.Api.Utils.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace AluguelIdeal.Api.Utils.Extensions
{
    public static class ExceptionExtensions
    {
        public static int GetHttpResponseStatus(this Exception exception) =>
            exception is HttpResponseException httpResponseException ?
                    httpResponseException.Status :
                    StatusCodes.Status500InternalServerError;

        public static byte[] GetHttpResponseBody(this Exception exception, IHostEnvironment environment)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception), "can't get http response body from null exception");

            object responseBody;
            if (environment.IsDevelopment())
                responseBody = new
                {
                    message = exception.Message,
                    type = exception.GetType().FullName,
                    stackTrace = exception.StackTrace,
                    data = exception.Data
                };
            else
                responseBody = new
                {
                    message = exception is HttpResponseException ? exception.Message : "an error occured"
                };

            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseBody));
        }
    }
}
