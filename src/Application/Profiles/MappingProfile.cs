using AluguelIdeal.Application.Dtos.Residences;
using AluguelIdeal.Application.Dtos.Residences.Flats;
using AluguelIdeal.Application.Dtos.Residences.Houses;
using AluguelIdeal.Application.Enums;
using AluguelIdeal.Application.Interactors.Residences.Commands;
using AluguelIdeal.Domain.Entities;

namespace AluguelIdeal.Application.Profiles
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Residence, ResidenceDto>()
                .Include<Flat, FlatDto>()
                .Include<House, HouseDto>();

            CreateMap<Flat, FlatDto>();
            CreateMap<House, HouseDto>();

            CreateMap<CreateResidenceCommand, Flat>()
                .ForAllMembers(opt => opt.PreCondition(cmd => cmd.ResidenceType == ResidenceType.Flat));

            CreateMap<CreateResidenceCommand, House>()
                .ForAllMembers(opt => opt.PreCondition(cmd => cmd.ResidenceType == ResidenceType.House));

            CreateMap<UpdateResidenceCommand, Flat>()
                .ForAllMembers(opt => opt.PreCondition(cmd => cmd.ResidenceType == ResidenceType.Flat));

            CreateMap<UpdateResidenceCommand, House>()
                .ForAllMembers(opt => opt.PreCondition(cmd => cmd.ResidenceType == ResidenceType.House));
        }
    }
}
