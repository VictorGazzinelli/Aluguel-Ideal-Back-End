using AluguelIdeal.Application.Services;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Priority;

namespace AluguelIdeal.IntegrationTests
{
    [Collection("CustomWebApplicationFactory collection")]
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    [DefaultPriority(0)]
    public abstract class IntegrationTestBase
    {
        protected CustomWebApplicationFactory fixture;
        protected IConfiguration configuration;
        protected IAuthService authService;
        protected IntegrationTestBase(CustomWebApplicationFactory fixture)
        {
            this.fixture = fixture;
            configuration = (IConfiguration)fixture.Services.GetService(typeof(IConfiguration));
            authService = (IAuthService)fixture.Services.GetService(typeof(IAuthService));
        }


    }
}
