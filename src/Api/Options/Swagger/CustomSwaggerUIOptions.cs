﻿using AluguelIdeal.Api.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI;
using static Microsoft.AspNetCore.Builder.SwaggerUIOptionsExtensions;

namespace AluguelIdeal.Api.Options.Swagger
{
    public class CustomSwaggerUIOptions : SwaggerUIOptions
    {
        public CustomSwaggerUIOptions()
        {
            this.SwaggerEndpoint("/swagger/v1/swagger.json", $"{FileExtensions.GetSolutionName()} API V1");
            RoutePrefix = string.Empty;
            this.DisplayRequestDuration();
            this.EnableFilter();
            this.EnableDeepLinking();
            this.DefaultModelsExpandDepth(-1);
        }
    }
}
