using Xunit;

namespace Api.UnitTests.Fixture.Collection
{
    [CollectionDefinition(nameof(GetClientsInteractorTestsCollectionFixture))]
    public class GetClientsInteractorTestsCollectionFixture : ICollectionFixture<GetClientsInteractorTestsFixture>
    {

    }
}
