using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Application.Services;
using AluguelIdeal.Infrastructure.Database.Access;
using AluguelIdeal.Infrastructure.Database.Migrations.Conventions;
using AluguelIdeal.Infrastructure.Database.Repositories;
using AluguelIdeal.Infrastructure.Extensions;
using AluguelIdeal.Infrastructure.Services;
using FluentMigrator;
using FluentMigrator.Infrastructure;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Conventions;
using FluentMigrator.Runner.Initialization;
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
        private static IServiceProvider CreateFluentMigrationServiceProvider(IConfiguration configuration, string environmentName)
        {
            IServiceCollection services = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(migrationRunnerBuilder => {
                    List<ConnectionStringSettings> connectionStringSettingsList =
                        configuration.GetSection(nameof(ConnectionStringSettingsCollection)).Get<List<ConnectionStringSettings>>();
                    if (connectionStringSettingsList.Any())
                        migrationRunnerBuilder.AddPostgres()
                            .WithGlobalConnectionString(connectionStringSettingsList.First().ConnectionString)
                            .WithVersionTable(new VersionTableMetaData())
                            .ScanIn(Assembly.GetExecutingAssembly()).For.All();
                })
                .Configure<FluentMigratorLoggerOptions>(x => x.ShowSql = true)
                .AddLogging(loggingBuilder => loggingBuilder.AddFluentMigratorConsole())
                .AddSingleton<ILoggerProvider, LogFileFluentMigratorLoggerProvider>()
                .Configure<RunnerOptions>(runnerOptions => runnerOptions.Tags = new[] { environmentName });

            services.AddSingleton<IConventionSet, NamingConventionSet>();

            return services.BuildServiceProvider(validateScopes: false);
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            services.Configure<List<ConnectionStringSettings>>(configuration.GetSection(nameof(ConnectionStringSettingsCollection)));
            AddRepositories(services);
            AddServices(services);
            UpdateDatabase(configuration, environment);

            return services;
        }

        private static void UpdateDatabase(IConfiguration configuration, IHostEnvironment environment)
        {
            IServiceProvider serviceProvider = CreateFluentMigrationServiceProvider(configuration, environment.EnvironmentName);
            using IServiceScope serviceScope = serviceProvider.CreateScope();
            IMigrationRunner migrationRunner = serviceScope.ServiceProvider.GetService<IMigrationRunner>();
            IMigrationInformationLoader migrationInformationLoader = migrationRunner.MigrationLoader;
            IMigrationContext migrationContext = serviceProvider.GetRequiredService<IMigrationContext>();
            IMigrationGenerator migrationGenerator = serviceProvider.GetRequiredService<IMigrationGenerator>();
            IMaintenanceLoader maintenanceLoader = serviceProvider.GetRequiredService<IMaintenanceLoader>();
            string sqlScriptsDirectory = Path.Combine(environment.ContentRootPath, "./Scripts/");

            if (!environment.IsProduction())
            {
                RefreshDirectory(sqlScriptsDirectory);

                migrationInformationLoader.CreateMigrationsSqlEquivalent(migrationContext, migrationGenerator, sqlScriptsDirectory);

                maintenanceLoader.CreateMigrationsSqlEquivalent(migrationContext, migrationGenerator, sqlScriptsDirectory);
            }

            migrationRunner.ListMigrations();

            if (environment.EnvironmentName.Equals("Testing"))
                migrationRunner.MigrateDown(0);

            migrationRunner.MigrateUp();
        }

        private static void RefreshDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, recursive: true);
                Directory.CreateDirectory(path);
            }
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IHashingService, HashingService>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IDistrictRepository, DistrictRepository>();
            services.AddTransient<IProfileRepository, ProfileRepository>();
            services.AddTransient<IResidenceRepository, ResidenceRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
