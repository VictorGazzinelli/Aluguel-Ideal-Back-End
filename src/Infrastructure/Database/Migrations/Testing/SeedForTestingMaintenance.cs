using AluguelIdeal.Domain.Entities;
using FluentMigrator;
using System;
using System.Collections.Generic;

namespace AluguelIdeal.Infrastructure.Database.Migrations.Testing
{
    [Maintenance(MigrationStage.AfterAll)]
    [Tags("Testing")]
    public class SeedForTestingMaintenance : Migration
    {
        public static IEnumerable<User> Users =>
            new User[]
            {
                new User() { Id = Guid.Parse("a298051c-b6af-11eb-8529-0242ac130003"), Name = "Admin", Email = "admin@mail.com", Password = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92" },
                new User() { Id = Guid.Parse("cc5e0018-44be-4bd4-8045-556541eb0b2e"), Name = "Landlord", Email = "landlord@mail.com", Password = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92" },
                new User() { Id = Guid.Parse("92145c8e-5f38-471b-9ded-e03b6c0a0767"), Name = "User", Email = "user@mail.com", Password = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92" },
            };

        public static IEnumerable<Role> Roles =>
            new Role[]
            {
                new Role() { Id = Guid.Parse("4cc1053a-b6af-11eb-8529-0242ac130003"), Name = "Admin" },
                new Role() { Id = Guid.Parse("4cc1092c-b6af-11eb-8529-0242ac130003"), Name = "Landlord" },
                new Role() { Id = Guid.Parse("4cc10b66-b6af-11eb-8529-0242ac130003"), Name = "User" },
            };

        public static IEnumerable<Profile> Profiles =>
            new Profile[]
            {
                new Profile() { UserId = Guid.Parse("a298051c-b6af-11eb-8529-0242ac130003"), RoleId = Guid.Parse("4cc1053a-b6af-11eb-8529-0242ac130003") },
                new Profile() { UserId = Guid.Parse("a298051c-b6af-11eb-8529-0242ac130003"), RoleId = Guid.Parse("4cc1092c-b6af-11eb-8529-0242ac130003") },
                new Profile() { UserId = Guid.Parse("a298051c-b6af-11eb-8529-0242ac130003"), RoleId = Guid.Parse("4cc10b66-b6af-11eb-8529-0242ac130003") },
                new Profile() { UserId = Guid.Parse("cc5e0018-44be-4bd4-8045-556541eb0b2e"), RoleId = Guid.Parse("4cc1092c-b6af-11eb-8529-0242ac130003") },
                new Profile() { UserId = Guid.Parse("cc5e0018-44be-4bd4-8045-556541eb0b2e"), RoleId = Guid.Parse("4cc10b66-b6af-11eb-8529-0242ac130003") },
                new Profile() { UserId = Guid.Parse("92145c8e-5f38-471b-9ded-e03b6c0a0767"), RoleId = Guid.Parse("4cc10b66-b6af-11eb-8529-0242ac130003") },
            };

        public static IEnumerable<City> Cities =>
            new City[]
            {
                new City() { Id = Guid.Parse("29068e9e-39ba-4bb4-b743-a7ec1c4c5be0"), Name = "Belo Horizonte" },
                new City() { Id = Guid.Parse("aa97dd1d-3536-467e-8879-088fa06f020a"), Name = "São Paulo" },
                new City() { Id = Guid.Parse("0e6fe16b-814e-4390-9ff2-3d3b304693b4"), Name = "Rio de Janeiro" },
            };

        public static IEnumerable<District> Districts =>
            new District[]
            {
                new District() { Id = Guid.Parse("8f408fe8-b9fd-4be7-9c67-219bf97c50c2"), CityId = Guid.Parse("29068e9e-39ba-4bb4-b743-a7ec1c4c5be0"), Name = "Santo Antônio" },
                new District() { Id = Guid.Parse("43259691-b040-4526-9a14-a41afdf2a68f"), CityId = Guid.Parse("29068e9e-39ba-4bb4-b743-a7ec1c4c5be0"), Name = "São Pedro" },
                new District() { Id = Guid.Parse("66d5b0e4-324e-4ad0-b1d0-4753823db163"), CityId = Guid.Parse("aa97dd1d-3536-467e-8879-088fa06f020a"), Name = "Moema" },
                new District() { Id = Guid.Parse("4f7cd01c-bf77-4f0a-aef8-2b8b8ba479f4"), CityId = Guid.Parse("aa97dd1d-3536-467e-8879-088fa06f020a"), Name = "Bela Vista" },
                new District() { Id = Guid.Parse("67781b14-13d2-49a4-905e-d7ccfcf43c20"), CityId = Guid.Parse("0e6fe16b-814e-4390-9ff2-3d3b304693b4"), Name = "Ipanema" },
                new District() { Id = Guid.Parse("786006a9-758b-44ea-8665-ab6d446d59f5"), CityId = Guid.Parse("0e6fe16b-814e-4390-9ff2-3d3b304693b4"), Name = "Leblon" }
            };

        public static IEnumerable<Residence> Residences =>
            new Residence[]
            {
                new Residence()
                {
                    Id = Guid.Parse("668c5b37-f50a-4684-bf09-73d5044ae369"),
                    DistrictId = Guid.Parse("8f408fe8-b9fd-4be7-9c67-219bf97c50c2"),
                    Street = "Rua Santo Antônio do Monte 09",
                    Bedrooms = 2,
                    Bathrooms = 1,
                    Area = 60,
                    Rent = 2000,
                    Tax = 300,
                    Description = "O apartamento com atrás privativas tem três quartos sendo uma suíte, em piso laminado de madeira." +
                    " Os banheiros, a cozinha, área de serviço, o quarto e banheiro de empregada são em granito." +
                    " A sala é em porcelanato. Este apartamento com área privativa tem tres vagas." +
                    " O prédio tem piscina, elevador com senha, hidrômetros e medidores de gás separados." +
                    " Além de portas maciças na cozinha e sala (de entrada no apartamento) e janelas com persianas externas que vedam totalmente a passagem da luz." +
                    " Tudo foi feito para ter um condomínio mais em conta." +
                    " Todos os apartamentos lá são de frente e já tem armários embutidos, box, espelhos, luminárias, e acessórios de banheiro." +
                    " Só não permitimos animais domésticos.",
                }
            }; 

        public override void Up()
        {
            CreateUsers();
            CreateRoles();
            CreateProfiles();
            CreateCities();
            CreateDistricts();
            CreateResidences();
        }


        public override void Down()
        {
            DeleteResidences();
            DeleteDistricts();
            DeleteCities();
            DeleteProfiles();
            DeleteRoles();
            DeleteUsers();
        }


        private void CreateUsers()
        {
            foreach (User user in Users)
                Insert.IntoTable(nameof(User).ToLower()).Row(user.AsTableRow());
        }

        private void CreateRoles()
        {
            foreach (Role role in Roles)
                Insert.IntoTable(nameof(Role).ToLower()).Row(role.AsTableRow());
        }

        private void CreateProfiles()
        {
            foreach (Profile profile in Profiles)
                Insert.IntoTable(nameof(Profile).ToLower()).Row(profile.AsTableRow());
        }

        private void CreateCities()
        {
            foreach (City city in Cities)
                Insert.IntoTable(nameof(City).ToLower()).Row(city.AsTableRow());
        }
        private void CreateDistricts()
        {
            foreach (District district in Districts)
                Insert.IntoTable(nameof(District).ToLower()).Row(district.AsTableRow());
        }

        private void CreateResidences()
        {
            foreach (Residence residence in Residences)
                Insert.IntoTable(nameof(Residence).ToLower()).Row(residence.AsTableRow());
        }

        private void DeleteResidences()
        {
            foreach (Residence residence in Residences)
                Delete.FromTable(nameof(Residence).ToLower()).Row(residence.AsTableRow());
        }

        private void DeleteDistricts()
        {
            foreach (District district in Districts)
                Delete.FromTable(nameof(District).ToLower()).Row(district.AsTableRow());
        }

        private void DeleteCities()
        {
            foreach (City city in Cities)
                Delete.FromTable(nameof(City).ToLower()).Row(city.AsTableRow());
        }
        private void DeleteProfiles()
        {
            foreach (Profile profile in Profiles)
                Delete.FromTable(nameof(Profile).ToLower()).Row(profile.AsTableRow());
        }

        private void DeleteRoles()
        {
            foreach (Role role in Roles)
                Delete.FromTable(nameof(Role).ToLower()).Row(role.AsTableRow());
        }

        private void DeleteUsers()
        {
            foreach (User user in Users)
                Delete.FromTable(nameof(User).ToLower()).Row(user.AsTableRow());
        }
    }
}
