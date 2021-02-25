using AluguelIdeal.Api.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Models.Advertisement
{
    public class GetAllAdvertisementsOutput
    {
        public List<AdvertisementDto> Advertisements { get; set; }
    }
}
