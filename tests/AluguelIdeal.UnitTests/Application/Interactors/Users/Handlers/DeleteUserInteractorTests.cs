using AluguelIdeal.Application.Interactors.Users.Commands;
using AluguelIdeal.Application.Interactors.Users.Handlers;
using AluguelIdeal.Application.Repositories;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Users.Handlers
{
    public class DeleteUserInteractorTests
    {
        [Fact(DisplayName = "When DeleteUserInteractor.Handle is called, it should call UserRepository.DeleteAsync")]
        public async Task Handle_Calls_DeleteAsync()
        {
            // Arrange
            DeleteUserCommand request = new DeleteUserCommand();
            AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
            mocker.GetMock<IUserRepository>()
                .Setup(repository => repository.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            DeleteUserInteractor sut = mocker.CreateInstance<DeleteUserInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IUserRepository>()
                .Verify(repository => repository.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once, failMessage: "DeleteAsync was not called once");
        }
    }
}
