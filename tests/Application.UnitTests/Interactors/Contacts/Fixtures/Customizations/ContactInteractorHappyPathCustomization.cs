using AluguelIdeal.Application.Dtos.Contacts;
using AluguelIdeal.Application.Interactors.Contacts.Requests;
using AluguelIdeal.Application.Interactors.Contacts.Responses;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using AutoFixture;
using Bogus;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AluguelIdeal.Application.UnitTests.Interactors.Contacts.Fixtures.Customizations
{
    public class ContactInteractorHappyPathCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            Faker faker = new Faker("pt_BR");
            fixture.Customize<Contact>(composer =>
                composer
                .With(contact => contact.DeletedAt, (DateTime?)null)
                .With(contact => contact.Id, faker.Random.Int(min: 1)));
            IEnumerable<Contact> contactReadEnumerable =
                fixture.Create<Contact[]>();
            Contact contact = fixture.Freeze<Contact>();
            Mock<IContactRepository> contactRepositoryMock =
                fixture.Freeze<Mock<IContactRepository>>();

            contactRepositoryMock.Setup(x => x.CreateAsync(contact, It.IsAny<CancellationToken>())).ReturnsAsync(contact.Id);
            contactRepositoryMock.Setup(x => x.ReadAsync(It.IsAny<CancellationToken>())).ReturnsAsync(contactReadEnumerable);
            contactRepositoryMock.Setup(x => x.GetByIdAsync(contact.Id, It.IsAny<CancellationToken>())).ReturnsAsync(contact);
            contactRepositoryMock.Setup(x => x.UpdateAsync(contact, It.IsAny<CancellationToken>()));
            contactRepositoryMock.Setup(x => x.DeleteAsync(contact.Id, It.IsAny<CancellationToken>()));

            fixture.Customize<InsertContactRequest>(request =>
                request
                .With(x => x.Name, contact.Name)
                .With(x => x.Email, contact.Email)
                .With(x => x.Phone, contact.Phone));

            fixture.Customize<InsertContactResponse>(response =>
                response.With(x => x.Contact, new ContactDto(contact)));

            fixture.Customize<GetContactResponse>(response =>
                response.With(x => x.Contacts, contactReadEnumerable.Select(c => new ContactDto(c)).ToList()));

            fixture.Customize<GetContactByIdRequest>(request =>
                request.With(x => x.Id, contact.Id));

            fixture.Customize<GetContactByIdResponse>(response =>
                response.With(x => x.Contact, new ContactDto(contact)));

            fixture.Customize<UpdateContactRequest>(request =>
                request
                .With(x => x.Id, contact.Id)
                .With(x => x.Name, contact.Name)
                .With(x => x.Email, contact.Email)
                .With(x => x.Phone, contact.Phone));

            fixture.Customize<UpdateContactResponse>(response =>
                response.With(x => x.Contact, new ContactDto(contact)));

            fixture.Customize<DeleteContactRequest>(request =>
                request.With(x => x.Id, contact.Id));
        }
    }
}
