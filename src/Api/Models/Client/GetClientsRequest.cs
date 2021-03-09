using MediatR;

namespace AluguelIdeal.Api.Models.Client
{
    public class GetClientsRequest : IRequest<GetClientsResponse>
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
