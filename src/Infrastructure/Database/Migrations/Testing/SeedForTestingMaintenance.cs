using AluguelIdeal.Domain.Entities;
using FluentMigrator;
using System;

namespace AluguelIdeal.Infrastructure.Database.Migrations.Testing
{
    [Maintenance(MigrationStage.AfterAll)]
    [Tags("Testing")]
    public class SeedForTestingMaintenance : Migration
    {
        public override void Up()
        {
            CreateCities();
        }
        public override void Down()
        {
            DeleteCities();
        }

        private void CreateCities()
        {
            Insert.IntoTable(nameof(City).ToLower())
                .Row(new City() { Id = Guid.Parse("29068e9e-39ba-4bb4-b743-a7ec1c4c5be0"), Name = "Belo Horizonte" }.AsTableRow())
                .Row(new City() { Id = Guid.Parse("aa97dd1d-3536-467e-8879-088fa06f020a"), Name = "São Paulo" }.AsTableRow())
                .Row(new City() { Id = Guid.Parse("0e6fe16b-814e-4390-9ff2-3d3b304693b4"), Name = "Rio de Janeiro" }.AsTableRow());
        }


        private void DeleteCities()
        {
            Delete.FromTable(nameof(City).ToLower())
                .Row(new City() { Id = Guid.Parse("29068e9e-39ba-4bb4-b743-a7ec1c4c5be0"), Name = "Belo Horizonte" }.AsTableRow())
                .Row(new City() { Id = Guid.Parse("aa97dd1d-3536-467e-8879-088fa06f020a"), Name = "São Paulo" }.AsTableRow())
                .Row(new City() { Id = Guid.Parse("0e6fe16b-814e-4390-9ff2-3d3b304693b4"), Name = "Rio de Janeiro" }.AsTableRow());
        }
    }
}
