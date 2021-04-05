using AluguelIdeal.Application.Dtos.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AluguelIdeal.Application.Interactors.Contacts.Responses
{
    public sealed class GetContactResponse
    {
        public List<ContactDto> Contacts { get; set; }

        public override bool Equals(object obj) =>
            obj is GetContactResponse response &&
            Contacts.SequenceEqual(response.Contacts);

        public override int GetHashCode() =>
            HashCode.Combine(Contacts);
    }
}
