using System;
using AdvertisementEntity = AluguelIdeal.Domain.Entities.Advertisement;

namespace AluguelIdeal.Application.Dto.Advertisements
{
    public class AdvertisementDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public AdvertisementDto()
        {
        
        }

        public AdvertisementDto(AdvertisementEntity advertisment)
        {
            Id = advertisment.Id;
            Title = advertisment.Title;
        }

        public override bool Equals(object obj) =>
            obj is AdvertisementDto dto &&
            Id == dto.Id &&
            Title.Equals(dto.Title);

        public override int GetHashCode() =>
            HashCode.Combine(Id,Title);
    }
}
