using AluguelIdeal.Api.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using static Microsoft.Extensions.DependencyInjection.SwaggerGenOptionsExtensions;

namespace AluguelIdeal.Api.Options.Swagger
{
    public static class CustomSwaggerGenOptions
    {
        private static readonly string version = "v1";
        private static readonly string xmlFileName = $"{FileUtils.GetSolutionName()}.xml";
        private static readonly string xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
        private static readonly OpenApiInfo info = new OpenApiInfo()
        {
            Title = $"{FileUtils.GetSolutionName()} API",
            Version = version,
        };
        private static readonly OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
        {
            Type = SecuritySchemeType.Http,
            Description = $"JWT Authorization header using the {JwtBearerDefaults.AuthenticationScheme} scheme.",
            Name = HeaderNames.Authorization,
            In = ParameterLocation.Header,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Reference = new OpenApiReference()
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
        private static readonly OpenApiSecurityRequirement securityRequirement = new OpenApiSecurityRequirement()
        {
            [securityScheme] = new List<string>()
        };

        public static Action<SwaggerGenOptions> SetupAction =>
            CustomSwaggerGenOptionsSetupAction;

        private static void CustomSwaggerGenOptionsSetupAction(SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.SwaggerDoc(version, info);
            //swaggerGenOptions.IncludeXmlComments(xmlFilePath);
            swaggerGenOptions.DescribeAllParametersInCamelCase();
            swaggerGenOptions.AddFluentValidationRules();
            swaggerGenOptions.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
            swaggerGenOptions.AddSecurityRequirement(securityRequirement);
        }
    }
}
