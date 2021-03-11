using AluguelIdeal.Api.Entities;
using AluguelIdeal.Api.Gateways.Interfaces;
using AluguelIdeal.Api.Interactors.Client;
using AluguelIdeal.Api.Models.Client;
using Api.UnitTests.Fixture;
using Api.UnitTests.Fixture.Collection;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Api.UnitTests.Interactors
{
    [Trait("Clients", "GetClientsInteractor")]
    [Collection(nameof(GetClientsInteractorTestsCollectionFixture))]
    public class GetClientsInteractorTests
    {
        private readonly GetClientsInteractorTestsFixture fixture;

        public GetClientsInteractorTests(GetClientsInteractorTestsFixture fixture) =>
            this.fixture = fixture;

        [Theory(DisplayName = "should return clients in clientsGateway")]
        [MemberData(nameof(GetClientsInteractorTestsFixture.Params), MemberType = typeof(GetClientsInteractorTestsFixture))]
        public async Task ShouldReturnClients(IEnumerable<Client> mockedClients, CancellationTokenSource cancellationTokenSource,
                                                GetClientsRequest request, GetClientsResponse expectedResponse)
        {
            // Arrange
            fixture.Mocker.GetMock<IClientGateway>()
                .Setup(ClientGateway => ClientGateway.GetByNameAsync(request.Name, cancellationTokenSource.Token))
                .ReturnsAsync(mockedClients.Where(client => client.Name.Equals(request.Name)))
                .Verifiable();
            fixture.Mocker.GetMock<IClientGateway>()
                .Setup(ClientGateway => ClientGateway.GetByEmailAsync(request.Email, cancellationTokenSource.Token))
                .ReturnsAsync(mockedClients.Where(client => client.Email.Equals(request.Email)))
                .Verifiable();
            // Act
            GetClientsResponse actualResponse =
                await fixture.Mocker.CreateInstance<GetClientsInteractor>()
                .Handle(request, cancellationTokenSource.Token);
            // Assert
            if(request.Name != null)
                fixture.Mocker.GetMock<IClientGateway>()
                    .Verify(mock => mock.GetByNameAsync(request.Name, cancellationTokenSource.Token), Times.Once(), "GetByNameAsync was not called");
            else if (request.Email != null)
                fixture.Mocker.GetMock<IClientGateway>()
                    .Verify(mock => mock.GetByEmailAsync(request.Email, cancellationTokenSource.Token), Times.Once(), "GetByEmailAsync was not called");
            actualResponse.Should()
                .Be(expectedResponse);
        }
    }
}
