using AluguelIdeal.Application.Dtos.Contacts;
using System;

namespace AluguelIdeal.Application.Interactors.Contacts.Responses
{
    public sealed class InsertContactResponse
    {
        public ContactDto Contact { get; set; }

        public override bool Equals(object obj) =>
            obj is InsertContactResponse response &&
            Equals(Contact, response.Contact);

        public bool EqualsIgnoreContactId(object obj) =>
            obj is InsertContactResponse response &&
            Contact.EqualsIgnoreId(response.Contact);

        public override int GetHashCode() =>
            HashCode.Combine(Contact);
    }
}
