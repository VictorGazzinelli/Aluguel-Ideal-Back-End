using MediatR;

namespace AluguelIdeal.Api.Interactors.Advertisement.Requests
{
    public class DeleteAdvertisementRequest : IRequest
    {
        public int Id { get; set; }
    }
}
