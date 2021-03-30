using System.Collections.Generic;

namespace AluguelIdeal.Application.Interactors.Contacts.Responses
{
    public sealed class GetContactResponse
    {
        public List<ContactDto> Contacts { get; set; }
    }
}
