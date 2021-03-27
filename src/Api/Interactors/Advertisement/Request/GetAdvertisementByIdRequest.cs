using AluguelIdeal.Api.Interactors.Advertisement.Response;
using MediatR;

namespace AluguelIdeal.Api.Interactors.Advertisement.Request
{
    public class GetAdvertisementByIdRequest : IRequest<GetAdvertisementByIdResponse>
    {
        public int Id { get; set; }
    }
}
