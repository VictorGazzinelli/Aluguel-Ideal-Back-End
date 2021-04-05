using AluguelIdeal.Application.Interactors.Contacts.Handlers;
using AluguelIdeal.Application.Interactors.Contacts.Requests;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Application.UnitTests.Interactors.Contacts.Fixtures;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.Application.UnitTests.Interactors.Contacts
{
    [Trait("Clients", "DeleteContactInteractor")]
    public class DeleteContactInteractorTests
    {
        [Theory(DisplayName = "happy path returns no exceptions")]
        [ContactInteractorHappyPathFixture]
        public void ReturnsNoExceptions(DeleteContactRequest request, DeleteContactInteractor sut)
        {
            // Act
            Func<Task> act = async () => await sut.Handle(request, default);
            // Assert
            act.Should().NotThrow();
        }

        [Theory(DisplayName = "should call contact repository delete method")]
        [ContactInteractorHappyPathFixture]
        public async Task ShouldCallContactRepositoryDeleteAsync([Frozen] Mock<IContactRepository> contactRepositoryMock,
            DeleteContactRequest request,
            DeleteContactInteractor sut)
        {
            // Act
            await sut.Handle(request, default);
            // Assert
            contactRepositoryMock.Verify(x => x.DeleteAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once(), "DeleteAsync was not called");
        }
    }
}
