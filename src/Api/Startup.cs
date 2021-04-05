using AluguelIdeal.Api.Controllers.Models.Responses.Http;
using AluguelIdeal.Api.Conventions;
using AluguelIdeal.Api.Filters;
using AluguelIdeal.Api.Middlewares;
using AluguelIdeal.Application;
using AluguelIdeal.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace AluguelIdeal.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddHttpContextAccessor();

            services.AddRouting(routeOptions => routeOptions.LowercaseUrls = true);

            services.Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
            {
                apiBehaviorOptions.SuppressModelStateInvalidFilter = true;
                apiBehaviorOptions.SuppressMapClientErrors = true;
            });

            services.AddControllers(mvcOptions =>
            {
                mvcOptions.Filters.Add<ValidationFilter>();
                mvcOptions.Filters.Add(new ProducesResponseTypeAttribute(typeof(BadRequestResponse), StatusCodes.Status400BadRequest));
                mvcOptions.Filters.Add(new ProducesResponseTypeAttribute(typeof(InternalServerErrorResponse), StatusCodes.Status500InternalServerError));
            })
            .AddFluentValidation(fluentValidationMvcConfiguration =>
            {
                fluentValidationMvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "AluguelIdeal API", Version = "v1" });
                string xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                swaggerGenOptions.IncludeXmlComments(xmlFilePath);
                swaggerGenOptions.AddFluentValidationRules();
            });

            services.AddApplication();
            services.AddInfrastructure(Configuration, Environment);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(ExceptionHandlerMiddleware.ExceptionHandler(Environment));

            app.UseRouting();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSwagger();

            app.UseSwaggerUI(swaggerUIOptions =>
            {
                swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "AluguelIdeal API V1");
                swaggerUIOptions.RoutePrefix = string.Empty;
                swaggerUIOptions.DisplayRequestDuration();
                swaggerUIOptions.EnableFilter();
                swaggerUIOptions.EnableDeepLinking();
                swaggerUIOptions.DefaultModelsExpandDepth(-1);
            });
        }
    }
}
