using AluguelIdeal.Api.Dto;
using AluguelIdeal.Api.Entities;
using AluguelIdeal.Api.Models.Client;
using AutoFixture;
using Moq;
using Moq.AutoMock;
using System.Collections.Generic;
using System.Threading;

namespace Api.UnitTests.Fixture
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
}
