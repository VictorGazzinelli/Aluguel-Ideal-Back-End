using AluguelIdeal.Api.Database;
using AluguelIdeal.Api.Interactors.Behaviours;
using AluguelIdeal.Api.Migrations;
using AluguelIdeal.Api.Repositories;
using AluguelIdeal.Api.Repositories.Interfaces;
using FluentMigrator.Runner;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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
            AssemblyScanner
                .FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
                .ForEach(validator => services.AddTransient(validator.InterfaceType, validator.ValidatorType));
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateRequest<,>));

            services.AddControllers();

            services.AddHttpContextAccessor();

            services.Configure<ApiBehaviorOptions>(options => options.SuppressMapClientErrors = true);

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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
