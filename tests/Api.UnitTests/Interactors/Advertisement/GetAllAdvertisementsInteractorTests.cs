using AluguelIdeal.Api.Dto;
using AluguelIdeal.Api.Gateways.Interfaces;
using AluguelIdeal.Api.Interactors.Advertisement;
using AluguelIdeal.Api.Models.Advertisement;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AdvertisementEntity = AluguelIdeal.Api.Entities.Advertisement;

namespace Api.UnitTests.Interactors.Advertisement
{
    public class GetAllAdvertisementsInteractorTestsFixture
    {
        public static IEnumerable<object[]> Params = new[]
        {
            new[]
            {
                new GetAllAdvertisementsRequest(),
            },
        };
        
        public AutoMocker Mocker = new AutoMocker(MockBehavior.Strict);
    }

    [CollectionDefinition(nameof(GetAllAdvertisementsInteractorTestsCollectionFixture))]
    public class GetAllAdvertisementsInteractorTestsCollectionFixture : ICollectionFixture<GetAllAdvertisementsInteractorTestsFixture>
    {

    }

    [Trait("Advertisement", "GetAllAdvertisementsInteractor")]
    [Collection(nameof(GetAllAdvertisementsInteractorTestsCollectionFixture))]
    public class GetAllAdvertisementsInteractorTests
    {
        private readonly GetAllAdvertisementsInteractorTestsFixture fixture;

        public GetAllAdvertisementsInteractorTests (GetAllAdvertisementsInteractorTestsFixture fixture) =>
            this.fixture = fixture;

        [Theory(DisplayName = "should return all Advertisements in advertisementGateway")]
        [MemberData(nameof(GetAllAdvertisementsInteractorTestsFixture.Params), MemberType = typeof(GetAllAdvertisementsInteractorTestsFixture))]
        public async Task ShouldReturnAllAdvertisements(GetAllAdvertisementsRequest request)
        {
            // Arrange
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            IEnumerable<AdvertisementEntity> mockedAdvertisements = new AdvertisementEntity[] {
                new AdvertisementEntity()
                {
                    Id = 1,
                    Title = "Title1",
                },
                new AdvertisementEntity()
                {
                    Id = 2,
                    Title = "Title2",
                }
            };
            List<AdvertisementDto> expectedAdvertisements = new List<AdvertisementDto> {
                new AdvertisementDto()
                {
                    Id = 1,
                    Title = "Title1",
                },
                new AdvertisementDto()
                {
                    Id = 2,
                    Title = "Title2",
                }
            };
            GetAllAdvertisementsResponse expectedResponse = new GetAllAdvertisementsResponse()
            {
                Advertisements = expectedAdvertisements
            };
            fixture.Mocker.GetMock<IAdvertisementGateway>()
                .Setup(advertisementGateway => advertisementGateway.GetAllAsync(cancellationTokenSource.Token))
                .ReturnsAsync(mockedAdvertisements);
            // Act
            GetAllAdvertisementsResponse actualResponse = 
                await fixture.Mocker.CreateInstance<GetAllAdvertisementsInteractor>()
                .Handle(request, cancellationTokenSource.Token);
            // Assert
            fixture.Mocker.GetMock<IAdvertisementGateway>()
                .Verify(mock => mock.GetAllAsync(cancellationTokenSource.Token), Times.Once(), "GetAllAsync not called once");
            actualResponse.Should()
                .Be(expectedResponse);
        }


    }
}
