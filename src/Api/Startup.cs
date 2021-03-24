using AluguelIdeal.Api.Conventions;
using AluguelIdeal.Api.Database;
using AluguelIdeal.Api.Filters;
using AluguelIdeal.Api.Interactors.Behaviours;
using AluguelIdeal.Api.Middlewares.Extensions;
using AluguelIdeal.Api.Migrations;
using AluguelIdeal.Api.Repositories;
using AluguelIdeal.Api.Repositories.Interfaces;
using FluentMigrator.Runner;
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

            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateRequest<,>));

            services.Configure<ApiBehaviorOptions>(apiBehaviorOptions => {
                apiBehaviorOptions.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers(mvcOptions => {
                    mvcOptions.Filters.Add<ValidationFilter>();
                    mvcOptions.Conventions.Add(new RouteTokenTransformerConvention(new SlugCaseRouteTransformer()));
                })
                .AddFluentValidation(fluentValidationMvcConfiguration => {
                    fluentValidationMvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

            services.AddHttpContextAccessor();

            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
            services.AddTransient<IAdvertisementRepository, AdvertisementRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();

            services.AddFluentMigratorCore();
            services.ConfigureRunner(migrationRunnerBuilder =>
            {
                string globalConnectionString = Configuration.GetSection("DatabaseSettings:Databases:0:Connection").Value;

                migrationRunnerBuilder.AddPostgres()
                .WithGlobalConnectionString(globalConnectionString)
                .ScanIn(Assembly.Load("AluguelIdeal.Api")).For.Migrations();
            });

            services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                swaggerGenOptions.AddFluentValidationRules();
            });
        }

        private static IServiceProvider CreateSerivces(IConfiguration configuration)
        {
            ServiceCollection services = new ServiceCollection();

            services.AddFluentMigratorCore();
            services.ConfigureRunner(migrationRunnerBuilder =>
            {
                string globalConnectionString = configuration.GetSection("DatabaseSettings:Databases:0:Connection").Value;

                migrationRunnerBuilder.AddPostgres()
                .WithGlobalConnectionString(globalConnectionString)
                .ScanIn(Assembly.Load("AluguelIdeal.Api")).For.Migrations();
            });

            return services.BuildServiceProvider(validateScopes: false);
        }

        private static void ResetDatabase(IServiceProvider serviceProvider)
        {
            IMigrationRunner migrationRunner = serviceProvider.GetService<IMigrationRunner>();
            migrationRunner.Up(new AluguelIdealMigration20210320001());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = CreateSerivces(Configuration);
            using (IServiceScope serviceScope = serviceProvider.CreateScope())
            {
                ResetDatabase(serviceScope.ServiceProvider);
            }

            app.UseSwagger();
            app.UseSwaggerUI(swaggerUIOptions =>
            {
                swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                swaggerUIOptions.RoutePrefix = string.Empty;
                swaggerUIOptions.DisplayRequestDuration();
                swaggerUIOptions.EnableFilter();
            });

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionHandler(Environment);
            app.UseRequestResponseLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
