using Xunit;

namespace AluguelIdeal.IntegrationTests
{
    [CollectionDefinition("CustomWebApplicationFactory collection")]
    public class CustomWebApplicationFactoryCollection : ICollectionFixture<CustomWebApplicationFactory>
    {
    }
}
