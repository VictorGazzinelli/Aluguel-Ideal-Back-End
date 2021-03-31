using AluguelIdeal.Application.Interactors.Advertisements.Responses;
using MediatR;

namespace AluguelIdeal.Application.Interactors.Advertisements.Requests
{
    public class GetAdvertisementByIdRequest : IRequest<GetAdvertisementByIdResponse>
    {
        public int Id { get; set; }
    }
}
