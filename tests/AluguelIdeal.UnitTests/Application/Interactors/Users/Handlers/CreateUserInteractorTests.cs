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
    public class CreateUserInteractorTests
    {
        [Fact(DisplayName = "When CreateUserInteractor.Handle is called, it should call UserRepository.CreateAsync")]
        public async Task Handle_Calls_CreateAsync()
        {
            // Arrange
            CreateUserCommand request = new CreateUserCommand();
            AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
            mocker.GetMock<IUserRepository>()
                .Setup(repository => repository.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            CreateUserInteractor sut = mocker.CreateInstance<CreateUserInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IUserRepository>()
                .Verify(repository => repository.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once, failMessage: "CreateAsync was not called once");
        }
    }
}
