using AluguelIdeal.Domain.Entities;

namespace AluguelIdeal.Application.Dtos.Residences.Houses
{
    public class HouseDto : ResidenceDto
    {
        public double YardArea { get; set; }

        public HouseDto() : base()
        {
            
        }
    }
}
