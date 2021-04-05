using AluguelIdeal.Application.Dtos.Advertisements;
using MediatR;

namespace AluguelIdeal.Application.Interactors.Advertisements.Queries
{
    public class GetAdvertisementsQuery : IRequest<QueryResult<AdvertisementDto>>
    {
        public string TitleContains { get; set; }
    }
}
