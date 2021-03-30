using MediatR;

namespace AluguelIdeal.Application.Interactors.Advertisements.Requests
{
    public class DeleteAdvertisementRequest : IRequest
    {
        public int Id { get; set; }
    }
}
