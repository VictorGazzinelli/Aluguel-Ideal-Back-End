using AluguelIdeal.Api.Middlewares;
using AluguelIdeal.Api.Options.ApiBehavior;
using AluguelIdeal.Api.Options.Authorization;
using AluguelIdeal.Api.Options.Cors;
using AluguelIdeal.Api.Options.Jwt;
using AluguelIdeal.Api.Options.Mvc;
using AluguelIdeal.Api.Options.Swagger;
using AluguelIdeal.Application;
using AluguelIdeal.Application.Transactions;
using AluguelIdeal.Infrastructure;
using AluguelIdeal.Infrastructure.Transactions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

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
            services.AddHttpContextAccessor();

            services.AddApplication();

            services.AddSingleton<ITransactionManager, TransactionManager>();

            services.AddInfrastructure(Configuration, Environment);

            services.AddCors(CustomCorsOptions.SetupAction);
            
            services.AddRouting(routeOptions => routeOptions.LowercaseUrls = true);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(CustomJwtBearerOptions.GetSetupAction(Configuration));

            services.AddAuthorization(CustomAuthorizationOptions.SetupAction);

            services.Configure(CustomApiBehaviourOptions.SetupAction);

            services.AddControllers(CustomMvcOptions.SetupAction)
            .AddFluentValidation(fluentValidationMvcConfiguration => fluentValidationMvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>())
            .AddJsonOptions(jsonOptions => jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

            services.AddSwaggerGen(CustomSwaggerGenOptions.SetupAction);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler(ExceptionHandlerMiddleware.ExceptionHandler());

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseSwagger();

            app.UseSwaggerUI(new CustomSwaggerUIOptions());

            app.UseEndpoints(endpointRouterBuilder => endpointRouterBuilder.MapControllers());
        }
    }
}
