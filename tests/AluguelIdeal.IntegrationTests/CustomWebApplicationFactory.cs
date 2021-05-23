using AluguelIdeal.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace AluguelIdeal.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");

            return base.CreateHostBuilder()
            .ConfigureHostConfiguration(config => config.AddEnvironmentVariables("ASPNETCORE"));
        }
    }
}
