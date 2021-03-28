using AluguelIdeal.Api.Dto.Advertisement;
using System.Collections.Generic;

namespace AluguelIdeal.Api.Interactors.Advertisement.Responses
{
    public class GetAdvertisementResponse
    {
        public List<AdvertisementDto> Advertisements { get; set; }
    }
}
