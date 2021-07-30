using System;

namespace AluguelIdeal.Application.Dtos.Residences.Flats
{
    public class FlatDto : ResidenceDto
    {
        public double Condominium { get; set; }
        public int Floor { get; set; }

        public FlatDto() : base()
        {

        }

        public override bool Equals(object obj)
        {
            return obj is FlatDto dto &&
                   base.Equals(obj) &&
                   Condominium == dto.Condominium &&
                   Floor == dto.Floor;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(Condominium);
            hash.Add(Floor);
            return hash.ToHashCode();
        }
    }
}
