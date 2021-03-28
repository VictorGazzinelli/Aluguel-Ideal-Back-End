using AluguelIdeal.Api.Interactors.Advertisement.Responses;
using MediatR;

namespace AluguelIdeal.Api.Interactors.Advertisement.Requests
{
    public class UpdateAdvertisementRequest : IRequest<UpdateAdvertisementResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
