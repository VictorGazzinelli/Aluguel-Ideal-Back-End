using AluguelIdeal.Application.Dtos.Contacts;
using System;

namespace AluguelIdeal.Application.Interactors.Contacts.Responses
{
    public sealed class UpdateContactResponse
    {
        public ContactDto Contact { get; set; }

        public override bool Equals(object obj) =>
            obj is UpdateContactResponse response &&
            Equals(Contact, response.Contact);

        public override int GetHashCode() =>
            HashCode.Combine(Contact);
    }
}
