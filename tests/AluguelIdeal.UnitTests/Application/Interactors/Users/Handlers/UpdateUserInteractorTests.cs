using AluguelIdeal.Application.Interactors.Users.Commands;
using AluguelIdeal.Application.Interactors.Users.Handlers;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using Moq;
using Moq.AutoMock;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Users.Handlers
{
    public class UpdateUserInteractorTests
    {
        [Fact(DisplayName = "When UpdateUserInteractor.Handle is called, it should call UserRepository.UpdateAsync")]
        public async Task Handle_Calls_UpdateAsync()
        {
            // Arrange
            UpdateUserCommand request = new UpdateUserCommand();
            AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
            mocker.GetMock<IUserRepository>()
                .Setup(repository => repository.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            UpdateUserInteractor sut = mocker.CreateInstance<UpdateUserInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IUserRepository>()
                .Verify(repository => repository.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once, failMessage: "UpdateAsync was not called once");
        }
    }
}
