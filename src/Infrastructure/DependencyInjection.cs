using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Infrastructure.Database.Access;
using AluguelIdeal.Infrastructure.Database.Migrations;
using AluguelIdeal.Infrastructure.Database.Repositories;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace AluguelIdeal.Infrastructure
{
    public static class DependencyInjection
    {
        private static IServiceProvider CreateServices(IConfiguration configuration)
        {
            return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(migrationRunnerBuilder =>
            {
                List<ConnectionStringSettings> connectionStringSettingsList =
                   configuration.GetSection(nameof(ConnectionStringSettingsCollection)).Get<List<ConnectionStringSettings>>();
                if (connectionStringSettingsList.Any())
                    migrationRunnerBuilder.AddPostgres()
                    .WithGlobalConnectionString(connectionStringSettingsList.First().ConnectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations();
            })
            .AddLogging(loggingBuilder => loggingBuilder.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
        }
            
        

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            services.Configure<List<ConnectionStringSettings>>(configuration.GetSection(nameof(ConnectionStringSettingsCollection)));
            services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
            services.AddTransient<IAdvertisementRepository, AdvertisementRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();

            if (environment.IsDevelopment())
            {
                IServiceProvider serviceProvider = CreateServices(configuration);
                using IServiceScope serviceScope = serviceProvider.CreateScope();
                IMigrationRunner migrationRunner = serviceScope.ServiceProvider.GetService<IMigrationRunner>();
                migrationRunner.Up(new AluguelIdealDatabaseMigration());
            }

            return services;
        }
    }
}
