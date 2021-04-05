using AluguelIdeal.Domain.Entities;
using System;

namespace AluguelIdeal.Application.Dtos.Advertisements
{
    public class AdvertisementDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public AdvertisementDto()
        {
        
        }

        public AdvertisementDto(Advertisement advertisment)
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
