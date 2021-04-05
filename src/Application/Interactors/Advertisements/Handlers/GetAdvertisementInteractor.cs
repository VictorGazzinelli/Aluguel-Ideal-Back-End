using AluguelIdeal.Application.Dtos.Advertisements;
using AluguelIdeal.Application.Interactors.Advertisements.Queries;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Advertisements.Handlers
{
    public class GetAdvertisementsInteractor : IRequestHandler<GetAdvertisementsQuery, QueryResult<AdvertisementDto>>
    {
        private readonly IAdvertisementRepository advertisementRepository;
        public GetAdvertisementsInteractor(IAdvertisementRepository advertisementRepository)
        {
            this.advertisementRepository = advertisementRepository;
        }

        public async Task<QueryResult<AdvertisementDto>> Handle(GetAdvertisementsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Advertisement> advertisements = await advertisementRepository.ReadAsync(cancellationToken);

            if (request.TitleContains != null)
                advertisements = advertisements.Where(a => a.Title.Contains(request.TitleContains));

            QueryResult<AdvertisementDto> result = new QueryResult<AdvertisementDto>()
            {
                Items = advertisements.Select(a => new AdvertisementDto(a))
            };

            return result;
        }
    }
}
