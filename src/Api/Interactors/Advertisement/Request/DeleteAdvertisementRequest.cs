using MediatR;

namespace AluguelIdeal.Api.Interactors.Advertisement.Request
{
    public class DeleteAdvertisementRequest : IRequest
    {
        public int Id { get; set; }
    }
}
