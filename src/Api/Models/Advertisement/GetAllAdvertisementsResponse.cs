using AluguelIdeal.Api.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AluguelIdeal.Api.Models.Advertisement
{
    public class GetAllAdvertisementsResponse
    {
        public List<AdvertisementDto> Advertisements { get; set; }

        public override bool Equals(object obj) =>
            obj is GetAllAdvertisementsResponse response &&
            Enumerable.SequenceEqual(Advertisements, response.Advertisements);

        public override int GetHashCode() =>
            HashCode.Combine(Advertisements);
    }
}
