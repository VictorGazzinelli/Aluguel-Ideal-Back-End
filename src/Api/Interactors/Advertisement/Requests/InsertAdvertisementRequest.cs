using AluguelIdeal.Api.Interactors.Advertisement.Responses;
using MediatR;

namespace AluguelIdeal.Api.Interactors.Advertisement.Requests
{
    public class InsertAdvertisementRequest : IRequest<InsertAdvertisementResponse>
    {
        public string Title { get; set; }
    }
}
