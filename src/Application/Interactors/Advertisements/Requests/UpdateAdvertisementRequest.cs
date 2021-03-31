using AluguelIdeal.Application.Interactors.Advertisements.Responses;
using MediatR;

namespace AluguelIdeal.Application.Interactors.Advertisements.Requests
{
    public class UpdateAdvertisementRequest : IRequest<UpdateAdvertisementResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
