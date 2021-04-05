using AluguelIdeal.Api.Interactors.Contacts.Handlers;
using AluguelIdeal.Application.Interactors.Contacts.Requests;
using AluguelIdeal.Application.Interactors.Contacts.Responses;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Application.UnitTests.Interactors.Contacts.Fixtures;
using AluguelIdeal.Domain.Entities;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.Application.UnitTests.Interactors.Contacts
{
    [Trait("Clients", "UpdateContactInteractor")]
    public class UpdateContactInteractorTests
    {
        [Theory(DisplayName = "happy path returns no exceptions")]
        [ContactInteractorHappyPathFixture]
        public void ReturnsNoExceptions(UpdateContactRequest request, UpdateContactInteractor sut)
        {
            // Act
            Func<Task> act = async () => await sut.Handle(request, default);
            // Assert
            act.Should().NotThrow();
        }

        [Theory(DisplayName = "happy path returns expected response")]
        [ContactInteractorHappyPathFixture]
        public async Task ReturnsExpectedResponse(UpdateContactRequest request, UpdateContactResponse expectedResponse, UpdateContactInteractor sut)
        {
            // Act
            UpdateContactResponse actualResponse = await sut.Handle(request, default);
            // Assert
            actualResponse.Should().Be(expectedResponse);
        }

        [Theory(DisplayName = "happy path should call contact repository update method")]
        [ContactInteractorHappyPathFixture]
        public async Task ShouldCallContactRepositoryUpdateAsync([Frozen] Mock<IContactRepository> contactRepositoryMock,
            Contact contact,
            UpdateContactRequest request,
            UpdateContactInteractor sut)
        {
            // Act
            await sut.Handle(request, default);
            // Assert
            contactRepositoryMock.Verify(x => x.UpdateAsync(contact, It.IsAny<CancellationToken>()), Times.Once(), "UpdateAsync was not called");
        }
    }
}
