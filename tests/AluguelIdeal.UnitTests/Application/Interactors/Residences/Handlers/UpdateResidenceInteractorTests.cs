using AluguelIdeal.Application.Interactors.Residences.Commands;
using AluguelIdeal.Application.Interactors.Residences.Handlers;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using Moq;
using Moq.AutoMock;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Residences.Handlers
{
    public class UpdateResidenceInteractorTests
    {
        [Fact(DisplayName = "When UpdateResidenceInteractor.Handle is called, it should call ResidenceRepository.UpdateAsync")]
        public async Task Handle_Calls_UpdateAsync()
        {
            // Arrange
            UpdateResidenceCommand request = new UpdateResidenceCommand();
            AutoMocker mocker = new AutoMocker(MockBehavior.Loose);
            mocker.GetMock<IResidenceRepository>()
                .Setup(repository => repository.UpdateAsync(It.IsAny<Residence>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            UpdateResidenceInteractor sut = mocker.CreateInstance<UpdateResidenceInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IResidenceRepository>()
                .Verify(repository => repository.UpdateAsync(It.IsAny<Residence>(), It.IsAny<CancellationToken>()), Times.Once, failMessage: "UpdateAsync was not called once");
        }
    }
}
