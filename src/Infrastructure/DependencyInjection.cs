using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Infrastructure.Database.Access;
using AluguelIdeal.Infrastructure.Database.Migrations;
using AluguelIdeal.Infrastructure.Database.Repositories;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
            .AddSingleton<ILoggerProvider, LogFileFluentMigratorLoggerProvider>()
            .Configure<LogFileFluentMigratorLoggerOptions>(opt => {
                string baseDirectory = Directory.GetParent(Environment.CurrentDirectory).FullName + @"\Infrastructure\Database\Migrations\Scripts";
                if (Directory.Exists(baseDirectory))
                    Directory.Delete(baseDirectory, true);
                Directory.CreateDirectory(baseDirectory);
                opt.OutputFileName = Path.Combine(baseDirectory, "aluguel_ideal_dev_latest_migration.sql");
                opt.ShowSql = true;
                opt.ShowElapsedTime = true;
            })
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
