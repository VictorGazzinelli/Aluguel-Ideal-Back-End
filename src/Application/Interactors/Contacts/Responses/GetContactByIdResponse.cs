﻿using AluguelIdeal.Application.Dtos.Contacts;
using System;

namespace AluguelIdeal.Application.Interactors.Contacts.Responses
{
    public class GetContactByIdResponse
    {
        public ContactDto Contact { get; set; }

        public override bool Equals(object obj) =>
            obj is GetContactByIdResponse response &&
            Equals(Contact, response.Contact);

        public override int GetHashCode() =>
            HashCode.Combine(Contact);
    }
}
