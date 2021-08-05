using AluguelIdeal.Application.Dtos.Residences;
using AluguelIdeal.Application.Dtos.Residences.Flats;
using AluguelIdeal.Application.Dtos.Residences.Houses;
using AluguelIdeal.Application.Dtos.Users;
using AluguelIdeal.Application.Interactors.Residences.Commands;
using AluguelIdeal.Application.Interactors.Users.Commands;
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

            CreateMap<CreateResidenceCommand, Flat>();
            CreateMap<CreateResidenceCommand, House>();

            CreateMap<UpdateResidenceCommand, Flat>();
            CreateMap<UpdateResidenceCommand, House>();

            CreateMap<CreateResidenceCommand, Residence>()
                .ConstructUsing((cmd, ctx) => cmd.ResidenceType.CreateResidence(cmd,ctx));

            CreateMap<UpdateResidenceCommand, Residence>()
                .ConstructUsing((cmd, ctx) => cmd.ResidenceType.CreateResidence(cmd, ctx));

            CreateMap<User, InsensitiveUserDto>();

            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();
            CreateMap<RegisterUserCommand, User>();
        }
    }
}
