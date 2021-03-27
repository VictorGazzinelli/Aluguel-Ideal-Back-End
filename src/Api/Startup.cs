using AluguelIdeal.Api.Controllers.Conventions;
using AluguelIdeal.Api.Database;
using AluguelIdeal.Api.Filters;
using AluguelIdeal.Api.Interactors.Behaviours;
using AluguelIdeal.Api.Middlewares.Extensions;
using AluguelIdeal.Api.Migrations;
using AluguelIdeal.Api.Repositories;
using AluguelIdeal.Api.Repositories.Interfaces;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

            services.Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
            {
                apiBehaviorOptions.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers(mvcOptions =>
            {
                mvcOptions.Filters.Add<ValidationFilter>();
                mvcOptions.Filters.Add<HandleExceptionFilter>();
                mvcOptions.Conventions.Add(new RouteTokenTransformerConvention(new SlugCaseRouteTransformer()));
            })
            .AddFluentValidation(fluentValidationMvcConfiguration =>
            {
                fluentValidationMvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                swaggerGenOptions.AddFluentValidationRules();
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateRequest<,>));

            services.Configure<List<ConnectionStringSettings>>(Configuration.GetSection(nameof(ConnectionStringSettingsCollection)));
            services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
            services.AddTransient<IAdvertisementRepository, AdvertisementRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();

            if (Environment.IsDevelopment())
            {
                services.AddFluentMigratorCore();
                services.ConfigureRunner(migrationRunnerBuilder =>
                {
                    List<ConnectionStringSettings> connectionStringSettingsList =
                       Configuration.GetSection(nameof(ConnectionStringSettingsCollection)).Get<List<ConnectionStringSettings>>();
                    if (connectionStringSettingsList.Any())
                        migrationRunnerBuilder.AddPostgres()
                        .WithGlobalConnectionString(connectionStringSettingsList.First().ConnectionString)
                        .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations();
                })
                .AddLogging(loggingBuilder => loggingBuilder.AddFluentMigratorConsole());
            }

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IServiceProvider serviceProvider = app.ApplicationServices;
                using IServiceScope serviceScope = serviceProvider.CreateScope();
                IMigrationRunner migrationRunner = serviceScope.ServiceProvider.GetService<IMigrationRunner>();
                migrationRunner.Up(new AluguelIdealDatabaseMigration());
            }

            app.UseCustomExceptionHandler(Environment);

            app.UseRequestResponseLogging();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSwagger();
            app.UseSwaggerUI(swaggerUIOptions =>
            {
                swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                swaggerUIOptions.RoutePrefix = string.Empty;
                swaggerUIOptions.DisplayRequestDuration();
                swaggerUIOptions.EnableFilter();
                swaggerUIOptions.EnableDeepLinking();
                swaggerUIOptions.DefaultModelsExpandDepth(-1);
            });
        }
    }
}
