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
    [Trait("Clients", "InsertContactInteractor")]
    public class InsertContactInteractorTests
    {
        [Theory(DisplayName = "happy path returns no exceptions")]
        [ContactInteractorHappyPathFixture]
        public void ReturnsNoExceptions(InsertContactRequest request, InsertContactInteractor sut)
        {
            // Act
            Func<Task> act = async () => await sut.Handle(request, default);
            // Assert
            act.Should().NotThrow();
        }

        [Theory(DisplayName = "happy path returns expected response")]
        [ContactInteractorHappyPathFixture]
        public async Task ReturnsExpectedResponse(InsertContactRequest request, InsertContactResponse expectedResponse, InsertContactInteractor sut)
        {
            // Act
            InsertContactResponse actualResponse = await sut.Handle(request, default);
            // Assert
            expectedResponse.EqualsIgnoreContactId(actualResponse).Should().BeTrue();
        }

        [Theory(DisplayName = "happy path should call contact repository create method")]
        [ContactInteractorHappyPathFixture]
        public async Task ShouldCallContactRepositoryCreateAsync([Frozen] Mock<IContactRepository> contactRepositoryMock,
            Contact contact,
            InsertContactRequest request,
            InsertContactInteractor sut)
        {
            // Act
            await sut.Handle(request, default);
            // Assert
            contactRepositoryMock.Verify(x => x.CreateAsync(It.Is<Contact>(c => c.EqualsIgnoreId(contact)), It.IsAny<CancellationToken>()), Times.Once(), "CreateAsync was not called");
        }
    }
}
