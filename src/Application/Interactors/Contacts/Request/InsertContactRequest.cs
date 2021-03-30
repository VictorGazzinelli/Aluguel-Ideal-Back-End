using MediatR;

namespace AluguelIdeal.Application.Interactors.Contacts.Request
{
    public  class InsertContactRequest : IRequest<InsertContactResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
