using AluguelIdeal.Application.Interactors.Residences.Handlers;
using AluguelIdeal.Application.Interactors.Residences.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Residences.Handlers
{
    public class GetResidenceByIdInteractorTests
    {
        [Fact(DisplayName = "When GetResidenceByIdInteractor.Handle is called, it should call ResidenceRepository.GetByIdAsync")]
        public async Task Handle_Calls_GetByIdAsync()
        {
            // Arrange
            GetResidenceByIdQuery request = new GetResidenceByIdQuery();
            AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
            mocker.GetMock<IResidenceRepository>()
                .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Residence())
                .Verifiable();
            GetResidenceByIdInteractor sut = mocker.CreateInstance<GetResidenceByIdInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IResidenceRepository>()
                .Verify(repository => repository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once, failMessage: "GetByIdAsync was not called once");
        }
    }
}
