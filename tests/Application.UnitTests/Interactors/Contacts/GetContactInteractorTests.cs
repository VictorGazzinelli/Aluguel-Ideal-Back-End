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
    [Trait("Clients", "GetContactInteractor")]
    public class GetContactInteractorTests
    {
        [Theory(DisplayName = "happy path returns no exceptions")]
        [ContactInteractorHappyPathFixture]
        public void ReturnsNoExceptions(GetContactRequest request, GetContactInteractor sut)
        {
            // Act
            Func<Task> act = async () => await sut.Handle(request, default);
            // Assert
            act.Should().NotThrow();
        }

        [Theory(DisplayName = "returns happy path expected response")]
        [ContactInteractorHappyPathFixture]
        public async Task ReturnsExpectedResponse(GetContactRequest request, GetContactResponse expectedResponse, GetContactInteractor sut)
        {
            // Act
            GetContactResponse actualResponse = await sut.Handle(request, default);
            // Assert
            actualResponse.Should().Be(expectedResponse);
        }

        [Theory(DisplayName = "should call contact repository read method")]
        [ContactInteractorHappyPathFixture]
        public async Task ShouldCallContactRepositoryReadAsync([Frozen] Mock<IContactRepository> contactRepositoryMock,
            GetContactRequest request,
            GetContactInteractor sut)
        {
            // Act
            await sut.Handle(request, default);
            // Assert
            contactRepositoryMock.Verify(x => x.ReadAsync(It.IsAny<CancellationToken>()), Times.Once(), "ReadAsync was not called");
        }
    }
}
