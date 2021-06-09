using AluguelIdeal.Application.Interactors.Profiles.Commands;
using AluguelIdeal.Application.Interactors.Profiles.Handlers;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using Moq;
using Moq.AutoMock;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Profiles.Handlers
{
    public class UnbindProfileInteractorTests
    {
        [Fact(DisplayName = "When UnbindProfileInteractor.Handle is called, it should call ProfileRepository.DeleteAsync")]
        public async Task Handle_Calls_DeleteAsync()
        {
            // Arrange
            UnbindProfileCommand request = new UnbindProfileCommand();
            AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
            mocker.GetMock<IProfileRepository>()
                .Setup(repository => repository.DeleteAsync(It.IsAny<Profile>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            UnbindProfileInteractor sut = mocker.CreateInstance<UnbindProfileInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IProfileRepository>()
                .Verify(repository => repository.DeleteAsync(It.IsAny<Profile>(), It.IsAny<CancellationToken>()), Times.Once, failMessage: "DeleteAsync was not called once");
        }
    }
}
