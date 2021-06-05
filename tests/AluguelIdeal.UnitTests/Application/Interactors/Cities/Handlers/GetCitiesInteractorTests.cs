using AluguelIdeal.Application.Interactors.Cities.Handlers;
using AluguelIdeal.Application.Interactors.Cities.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using Moq;
using Moq.AutoMock;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Cities.Handlers
{
    public class GetCitiesInteractorTests
    {
        [Fact(DisplayName = "When GetCitiesInteractor.Handle is called, it should call CityRepository.ReadAsync")]
        public async Task Handle_Calls_ReadAsync()
        {
            // Arrange
            GetCitiesQuery request = new GetCitiesQuery();
            AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
            mocker.GetMock<ICityRepository>()
                .Setup(repository => repository.ReadAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new City[0])
                .Verifiable();
            GetCitiesInteractor sut = mocker.CreateInstance<GetCitiesInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<ICityRepository>()
                .Verify(service => service.ReadAsync(It.IsAny<CancellationToken>()), Times.Once, failMessage: "ReadAsync was not called once");
        }
    }

}
