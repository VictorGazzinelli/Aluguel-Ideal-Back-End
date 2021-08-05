using AluguelIdeal.Domain.Entities;
using Ardalis.SmartEnum;
using AutoMapper;
using System;

namespace AluguelIdeal.Application.Enums
{
    public abstract class ResidenceType : SmartEnum<ResidenceType>
    {
        private ResidenceType(string name, int value) : base(name, value)
        {
                
        }

        public static readonly ResidenceType Flat = new FlatType();
        public static readonly ResidenceType House = new HouseType();

        public abstract Type ResidenceSubclass { get; }

        private void EnsureSubClassOfResidence()
        {
            if (!ResidenceSubclass.IsSubclassOf(typeof(Residence)))
                throw new InvalidCastException($"{ResidenceSubclass.Name} is not a subclass of Residence");
        }

        public Residence CreateResidence(object source, ResolutionContext resolutionContext)
        {
            EnsureSubClassOfResidence();

            return resolutionContext.Mapper.Map(source, source.GetType(), ResidenceSubclass) as Residence;
        }

        private class FlatType : ResidenceType
        {
            public FlatType() : base(nameof(Flat), 1)
            {

            }

            public override Type ResidenceSubclass => typeof(Flat);
        }

        private class HouseType : ResidenceType
        {
            public HouseType() : base(nameof(House), 2)
            {

            }

            public override Type ResidenceSubclass => typeof(House);
        }
    }
}
