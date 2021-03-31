using AluguelIdeal.Application.Dto.Advertisements;
using System.Collections.Generic;

namespace AluguelIdeal.Application.Interactors.Advertisements.Responses
{
    public class GetAdvertisementResponse
    {
        public List<AdvertisementDto> Advertisements { get; set; }
    }
}
