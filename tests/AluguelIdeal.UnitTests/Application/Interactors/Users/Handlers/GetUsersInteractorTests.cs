using AluguelIdeal.Application.Interactors.Users.Handlers;
using AluguelIdeal.Application.Interactors.Users.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using Moq;
using Moq.AutoMock;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Users.Handlers
{
    public class GetUsersInteractorTests
    {
        [Fact(DisplayName = "When GetUsersInteractor.Handle is called, it should call UserRepository.ReadAsync")]
        public async Task Handle_Calls_ReadAsync()
        {
            // Arrange
            GetUsersQuery request = new GetUsersQuery();
            AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
            mocker.GetMock<IUserRepository>()
                .Setup(repository => repository.ReadAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User[0])
                .Verifiable();
            GetUsersInteractor sut = mocker.CreateInstance<GetUsersInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IUserRepository>()
                .Verify(repository => repository.ReadAsync(It.IsAny<CancellationToken>()), Times.Once, failMessage: "ReadAsync was not called once");
        }
    }
}
