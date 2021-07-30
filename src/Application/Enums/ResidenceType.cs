
using Ardalis.SmartEnum;

namespace AluguelIdeal.Application.Enums
{
    public class ResidenceType : SmartEnum<ResidenceType>
    {
        public static readonly ResidenceType Flat = new ResidenceType(nameof(Flat), 1);
        public static readonly ResidenceType House = new ResidenceType(nameof(House), 2);
        
        public ResidenceType(string name, int value) : base(name, value)
        {
                
        }
    }
}
