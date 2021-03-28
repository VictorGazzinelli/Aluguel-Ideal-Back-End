using AluguelIdeal.Api.Interactors.Advertisement.Responses;
using MediatR;

namespace AluguelIdeal.Api.Interactors.Advertisement.Requests
{
    public class GetAdvertisementByIdRequest : IRequest<GetAdvertisementByIdResponse>
    {
        public int Id { get; set; }
    }
}
