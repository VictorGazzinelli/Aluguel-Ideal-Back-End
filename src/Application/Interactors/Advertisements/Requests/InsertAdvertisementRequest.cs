using MediatR;

namespace AluguelIdeal.Application.Interactors.Advertisements.Requests
{
    public class InsertAdvertisementRequest : IRequest<InsertAdvertisementResponse>
    {
        public string Title { get; set; }
    }
}
