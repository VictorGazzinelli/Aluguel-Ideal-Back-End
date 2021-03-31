using AluguelIdeal.Application.Interactors.Contacts.Responses;
using MediatR;

namespace AluguelIdeal.Application.Interactors.Contacts.Requests
{
    public class UpdateContactRequest : IRequest<UpdateContactResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
