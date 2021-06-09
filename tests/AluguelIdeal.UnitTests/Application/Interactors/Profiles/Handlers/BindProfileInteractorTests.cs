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
    public class BindProfileInteractorTests
    {
        [Fact(DisplayName = "When BindProfileInteractor.Handle is called, it should call ProfileRepository.CreateAsync")]
        public async Task Handle_Calls_CreateAsync()
        {
            // Arrange
            BindProfileCommand request = new BindProfileCommand();
            AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
            mocker.GetMock<IProfileRepository>()
                .Setup(repository => repository.CreateAsync(It.IsAny<Profile>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            BindProfileInteractor sut = mocker.CreateInstance<BindProfileInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IProfileRepository>()
                .Verify(repository => repository.CreateAsync(It.IsAny<Profile>(), It.IsAny<CancellationToken>()), Times.Once, failMessage: "CreateAsync was not called once");
        }
    }
}
