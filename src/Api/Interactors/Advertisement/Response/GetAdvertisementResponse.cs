using AluguelIdeal.Api.Dto;
using System.Collections.Generic;

namespace AluguelIdeal.Api.Interactors.Advertisement.Response
{
    public class GetAdvertisementResponse
    {
        public List<AdvertisementDto> Advertisements { get; set; }
    }
}
