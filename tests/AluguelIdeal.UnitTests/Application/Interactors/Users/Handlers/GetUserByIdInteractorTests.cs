using AluguelIdeal.Application.Interactors.Users.Handlers;
using AluguelIdeal.Application.Interactors.Users.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Users.Handlers
{
    public class GetUserByIdInteractorTests
    {
        [Fact(DisplayName = "When GetUserByIdInteractor.Handle is called, it should call UserRepository.GetByIdAsync")]
        public async Task Handle_Calls_GetByIdAsync()
        {
            // Arrange
            GetUserByIdQuery request = new GetUserByIdQuery();
            AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
            mocker.GetMock<IUserRepository>()
                .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User())
                .Verifiable();
            GetUserByIdInteractor sut = mocker.CreateInstance<GetUserByIdInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IUserRepository>()
                .Verify(repository => repository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once, failMessage: "GetByIdAsync was not called once");
        }
    }
}
