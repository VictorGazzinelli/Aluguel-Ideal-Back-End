using MediatR;

namespace AluguelIdeal.Api.Interactors.Advertisement.Request
{
    public sealed class DeleteAdvertisementRequest : IRequest
    {
        public int Id { get; set; }
    }
}
