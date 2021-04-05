
using AluguelIdeal.Api.Interactors.Contacts.Handlers;
using AluguelIdeal.Application.Interactors.Contacts.Requests;
using AluguelIdeal.Application.Interactors.Contacts.Responses;
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
    [Trait("Clients", "GetClientsInteractor")]
    public class GetContactByIdInteractorTests
    {
        [Theory(DisplayName = "happy path returns no exceptions")]
        [ContactInteractorHappyPathFixture]
        public void ReturnsNoExceptions(GetContactByIdRequest request, GetContactByIdInteractor sut)
        {
            // Act
            Func<Task> act = async () => await sut.Handle(request, default);
            // Assert
            act.Should().NotThrow();
        }

        [Theory(DisplayName = "happy path returns expected response")]
        [ContactInteractorHappyPathFixture]
        public async Task ReturnsExpectedResponse(GetContactByIdRequest request, GetContactByIdResponse expectedResponse, GetContactByIdInteractor sut)
        {
            // Act
            GetContactByIdResponse actualResponse = await sut.Handle(request, default);
            // Assert
            actualResponse.Should().Be(expectedResponse);
        }

        [Theory(DisplayName = "happy path should call contact repository get by id method")]
        [ContactInteractorHappyPathFixture]
        public async Task ShouldCallContactRepositoryGetByIdAsync([Frozen] Mock<IContactRepository> contactRepositoryMock,
            GetContactByIdRequest request,
            GetContactByIdInteractor sut)
        {
            // Act
            await sut.Handle(request, default);
            // Assert
            contactRepositoryMock.Verify(x => x.GetByIdAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once(), "GetByIdAsync was not called");
        }
    }
}