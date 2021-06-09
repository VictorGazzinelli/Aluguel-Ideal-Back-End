using AluguelIdeal.Application.Interactors.Districts.Handlers;
using AluguelIdeal.Application.Interactors.Districts.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using Moq;
using Moq.AutoMock;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Districts.Handlers
{
    public class GetDistrictsInteractorTests
    {
        [Fact(DisplayName = "When GetDistrictsInteractor.Handle is called, it should call DistrictRepository.ReadAsync")]
        public async Task Handle_Calls_ReadAsync()
        {
            // Arrange
            GetDistrictsQuery request = new GetDistrictsQuery();
            AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
            mocker.GetMock<IDistrictRepository>()
                .Setup(repository => repository.ReadAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new District[0])
                .Verifiable();
            GetDistrictsInteractor sut = mocker.CreateInstance<GetDistrictsInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IDistrictRepository>()
                .Verify(repository => repository.ReadAsync(It.IsAny<CancellationToken>()), Times.Once, failMessage: "ReadAsync was not called once");
        }
    }
}
