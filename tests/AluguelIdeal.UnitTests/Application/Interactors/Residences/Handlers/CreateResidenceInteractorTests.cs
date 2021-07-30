using AluguelIdeal.Application.Interactors.Residences.Commands;
using AluguelIdeal.Application.Interactors.Residences.Handlers;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using AutoMapper;
using Moq;
using Moq.AutoMock;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Residences.Handlers
{
    public class CreateResidenceInteractorTests
    {
        [Fact(DisplayName = "When CreateResidenceInteractor.Handle is called, it should call ResidenceRepository.CreateAsync")]
        public async Task Handle_Calls_CreateAsync()
        {
            // Arrange
            CreateResidenceCommand request = new CreateResidenceCommand();
            AutoMocker mocker = new AutoMocker(MockBehavior.Loose);
            mocker.GetMock<IResidenceRepository>()
                .Setup(repository => repository.CreateAsync(It.IsAny<Residence>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            CreateResidenceInteractor sut = mocker.CreateInstance<CreateResidenceInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IResidenceRepository>()
                .Verify(repository => repository.CreateAsync(It.IsAny<Residence>(), It.IsAny<CancellationToken>()), Times.Once, failMessage: "CreateAsync was not called once");
        }
    }
}
