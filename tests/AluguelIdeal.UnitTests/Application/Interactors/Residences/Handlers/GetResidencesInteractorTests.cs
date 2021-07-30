using AluguelIdeal.Application.Interactors.Residences.Handlers;
using AluguelIdeal.Application.Interactors.Residences.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using Moq;
using Moq.AutoMock;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Residences.Handlers
{
    public class GetResidencesInteractorTests
    {
        [Fact(DisplayName = "When GetResidencesInteractor.Handle is called, it should call ResidenceRepository.ReadAsync")]
        public async Task Handle_Calls_ReadAsync()
        {
            // Arrange
            GetResidencesQuery request = new GetResidencesQuery();
            AutoMocker mocker = new AutoMocker(MockBehavior.Loose);
            mocker.GetMock<IResidenceRepository>()
                .Setup(repository => repository.ReadAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Residence[0])
                .Verifiable();
            GetResidencesInteractor sut = mocker.CreateInstance<GetResidencesInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IResidenceRepository>()
                .Verify(repository => repository.ReadAsync(It.IsAny<CancellationToken>()), Times.Once, failMessage: "ReadAsync was not called once");
        }
    }
}
