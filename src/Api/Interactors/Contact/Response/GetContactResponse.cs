using AluguelIdeal.Api.Dto;
using System.Collections.Generic;

namespace AluguelIdeal.Api.Interactors.Contact.Response
{
    public sealed class GetContactResponse
    {
        public List<ContactDto> Contacts { get; set; }
    }
}
