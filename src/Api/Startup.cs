using AluguelIdeal.Api.Database;
using AluguelIdeal.Api.Repositories;
using AluguelIdeal.Api.Repositories.Interfaces;
using FluentMigrator.Runner;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            migrationRunner.MigrateUp(2021_03_20_001);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = CreateSerivces(Configuration);
            using (IServiceScope serviceScope = serviceProvider.CreateScope())
            {
                ResetDatabase(serviceScope.ServiceProvider);
            }

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
