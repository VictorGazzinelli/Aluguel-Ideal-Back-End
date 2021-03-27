using AluguelIdeal.Api.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace AluguelIdeal.Api.Filters
{
    public class HandleExceptionFilter : IActionFilter
    {
        private readonly IHostEnvironment environment;

        public HandleExceptionFilter(IHostEnvironment environment)
        {
            this.environment = environment;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception != null)
            {
                Console.WriteLine(context.Exception.Message);
                Console.WriteLine(context.Exception.StackTrace);
                Console.WriteLine(nameof(context.Exception.Data));
                foreach (DictionaryEntry entry in context.Exception.Data)
                    WriteEntry(entry);
                (int statusCode, Dictionary<string, object> responseBody) =
                    new ExceptionParser(environment).AsHttpResponse(context.Exception);
                byte[] responseBodyContent = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseBody));
                context.HttpContext.Response.StatusCode = statusCode;
                context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
                context.HttpContext.Response.Body.WriteAsync(responseBodyContent, 0, responseBodyContent.Length).Wait();
            }
        }

        private static void WriteEntry(DictionaryEntry entry)
        {
            string value = entry.Value is IEnumerable<object> enumerable ?
                string.Join(",", enumerable.Select(o => $"\"{o}\"")) :
                entry.Value.ToString() ?? "null";

            Console.WriteLine($"[{entry.Key}] = {value}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Ignore
        }
    }
}
