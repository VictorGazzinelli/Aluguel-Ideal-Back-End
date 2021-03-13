using AluguelIdeal.Api.Entities;
using System;

namespace AluguelIdeal.Api.Dto
{
    public class AdvertisementDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public AdvertisementDto()
        {
        
        }

        public AdvertisementDto(Advertisement advertisment)
        {
            this.Id = advertisment.Id;
            this.Title = advertisment.Title;
        }

        public override bool Equals(object obj) =>
            obj is AdvertisementDto dto &&
            Id == dto.Id &&
            Title.Equals(dto.Title);

        public override int GetHashCode() =>
            HashCode.Combine(Id,Title);
    }
}
