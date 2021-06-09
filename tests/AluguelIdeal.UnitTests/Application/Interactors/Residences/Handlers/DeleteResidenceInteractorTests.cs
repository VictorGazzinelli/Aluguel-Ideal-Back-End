using AluguelIdeal.Application.Interactors.Residences.Commands;
using AluguelIdeal.Application.Interactors.Residences.Handlers;
using AluguelIdeal.Application.Repositories;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Residences.Handlers
{
    public class DeleteResidenceInteractorTests
    {
        [Fact(DisplayName = "When DeleteResidenceInteractor.Handle is called, it should call ResidenceRepository.DeleteAsync")]
        public async Task Handle_Calls_DeleteAsync()
        {
            // Arrange
            DeleteResidenceCommand request = new DeleteResidenceCommand();
            AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
            mocker.GetMock<IResidenceRepository>()
                .Setup(repository => repository.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            DeleteResidenceInteractor sut = mocker.CreateInstance<DeleteResidenceInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IResidenceRepository>()
                .Verify(repository => repository.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once, failMessage: "DeleteAsync was not called once");
        }
    }
}
