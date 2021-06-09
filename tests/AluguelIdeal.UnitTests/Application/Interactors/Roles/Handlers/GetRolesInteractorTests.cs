using AluguelIdeal.Application.Interactors.Roles.Handlers;
using AluguelIdeal.Application.Interactors.Roles.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using Moq;
using Moq.AutoMock;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Roles.Handlers
{
    public class GetRolesInteractorTests
    {
        [Fact(DisplayName = "When GetRolesInteractor.Handle is called, it should call RoleRepository.ReadAsync")]
        public async Task Handle_Calls_ReadAsync()
        {
            // Arrange
            GetRolesQuery request = new GetRolesQuery();
            AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
            mocker.GetMock<IRoleRepository>()
                .Setup(repository => repository.ReadAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Role[0])
                .Verifiable();
            GetRolesInteractor sut = mocker.CreateInstance<GetRolesInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IRoleRepository>()
                .Verify(repository => repository.ReadAsync(It.IsAny<CancellationToken>()), Times.Once, failMessage: "ReadAsync was not called once");
        }
    }
}
