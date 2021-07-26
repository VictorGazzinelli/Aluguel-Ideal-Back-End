using AluguelIdeal.Domain.Entities;
using AluguelIdeal.Infrastructure.Database.Migrations.Attributes;
using FluentMigrator;
using System;

namespace AluguelIdeal.Infrastructure.Database.Migrations
{
    [MigrationTimestampVersion(
        description: "Adaptation of House and Flat entities to be polymorphic of Residence",
        transactionBehavior: TransactionBehavior.Default,
        year: 2021, month: 7, day: 18,
        hour: 12, minute: 4, second: 22)]
    [Tags(TagBehavior.RequireAny, "Testing", "Development", "Staging", "Production")]
    public class AdaptHouseFlatPolymorphismMigration : Migration
    {
        public override void Up()
        {
            DropOldFlatTable();
            DropOldHouseTable();
            CreateNewFlatTable();
            CreateNewHouseTable();
        }

        public override void Down()
        {
            DeleteNewHouseTable();
            DeleteNewFlatTable();
            CreateOldHouseTable();
            CreateOldFlatTable();
        }

        private void DeleteNewFlatTable()
        {
            Delete.Table(nameof(Flat).ToLower());
        }

        private void DeleteNewHouseTable()
        {
            Delete.Table(nameof(House).ToLower());
        }

        private void CreateNewFlatTable()
        {
            Create.Table(nameof(Flat).ToLower())
            .WithColumn(nameof(Flat.Id)).AsGuid().ForeignKey(nameof(Residence).ToLower(), nameof(Residence.Id)).NotNullable()
            .WithColumn(nameof(Flat.Condominium)).AsDouble().NotNullable()
            .WithColumn(nameof(Flat.Floor)).AsInt32().NotNullable();

            Create.PrimaryKey("PK_flat")
                .OnTable(nameof(Flat).ToLower())
                .Column(nameof(Flat.Id));
        }

        private void CreateNewHouseTable()
        {
            Create.Table(nameof(House).ToLower())
            .WithColumn(nameof(House.Id)).AsGuid().ForeignKey(nameof(Residence).ToLower(), nameof(Residence.Id)).NotNullable()
            .WithColumn(nameof(House.YardArea)).AsDouble().NotNullable();

            Create.PrimaryKey("PK_house")
                .OnTable(nameof(House).ToLower())
                .Column(nameof(House.Id));
        }

        private void CreateOldHouseTable() =>
            Create.Table(nameof(House).ToLower())
            .WithColumn(nameof(House.Id)).AsGuid().PrimaryKey()
            .WithColumn("residence_id").AsGuid().ForeignKey(nameof(Residence).ToLower(), nameof(Residence.Id)).NotNullable()
            .WithColumn(nameof(House.YardArea)).AsDouble().NotNullable();

        private void DropOldHouseTable() =>
            Delete.Table(nameof(House).ToLower());

        private void CreateOldFlatTable() =>
            Create.Table(nameof(Flat).ToLower())
            .WithColumn(nameof(Flat.Id)).AsGuid().PrimaryKey()
            .WithColumn("residence_id").AsGuid().ForeignKey(nameof(Residence).ToLower(), nameof(Residence.Id)).NotNullable()
            .WithColumn(nameof(Flat.Condominium)).AsDouble().NotNullable()
            .WithColumn(nameof(Flat.Floor)).AsInt32().NotNullable();

        private void DropOldFlatTable() =>
            Delete.Table(nameof(Flat).ToLower());
    }
}
