using AluguelIdeal.Api.Dto.Contact;
using System.Collections.Generic;

namespace AluguelIdeal.Api.Interactors.Contact.Response
{
    public sealed class GetContactResponse
    {
        public List<ContactDto> Contacts { get; set; }
    }
}
