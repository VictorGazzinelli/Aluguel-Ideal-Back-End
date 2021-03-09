using AluguelIdeal.Api.Dto;
using AluguelIdeal.Api.Entities;
using AluguelIdeal.Api.Gateways.Interfaces;
using AluguelIdeal.Api.Interactors.Client;
using AluguelIdeal.Api.Models.Client;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Api.UnitTests.Interactors
{
    public class GetClientsInteractorTestsFixture
    {
        public AutoMocker Mocker = new AutoMocker(MockBehavior.Strict);

        public static IEnumerable<Client> mockedClients = new Client[] {
                new Client()
                {
                    ClientId = 1,
                    Name = "Victor",
                    Email = "someEmail@someProvider.com"
                },
            };

        public static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public static IEnumerable<object[]> Params = new[]
        {
            new object[]
            {
                mockedClients,
                cancellationTokenSource,
                new GetClientsRequest(),
                new GetClientsResponse()
            },

            new object[]
            {
                mockedClients,
                cancellationTokenSource,
                new GetClientsRequest(){ Name = "Victor" },
                new GetClientsResponse(){ Clients = new List<ClientDto>()
                    {
                        new ClientDto()
                        {
                            Id = 1,
                            Name = "Victor",
                            Email = "someEmail@someProvider.com"
                        }
                    }
                }
            },

            new object[]
            {
                mockedClients,
                cancellationTokenSource,
                new GetClientsRequest(){ Email = "someEmail@someProvider.com"},
                new GetClientsResponse(){ Clients = new List<ClientDto>()
                    {
                        new ClientDto()
                        {
                            Id = 1,
                            Name = "Victor",
                            Email = "someEmail@someProvider.com"
                        }
                    }
                }
            },
        };

    }

    [CollectionDefinition(nameof(GetClientsInteractorTestsCollectionFixture))]
    public class GetClientsInteractorTestsCollectionFixture : ICollectionFixture<GetClientsInteractorTestsFixture>
    {

    }

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
