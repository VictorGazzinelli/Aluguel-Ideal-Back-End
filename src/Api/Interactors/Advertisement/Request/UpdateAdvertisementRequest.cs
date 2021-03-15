using AluguelIdeal.Api.Interactors.Advertisement.Response;
using MediatR;

namespace AluguelIdeal.Api.Interactors.Advertisement.Request
{
    public sealed class UpdateAdvertisementRequest : IRequest<UpdateAdvertisementResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
