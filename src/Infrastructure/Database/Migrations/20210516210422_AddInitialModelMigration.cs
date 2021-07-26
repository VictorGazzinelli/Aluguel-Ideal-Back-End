using AluguelIdeal.Domain.Entities;
using AluguelIdeal.Infrastructure.Database.Migrations.Attributes;
using FluentMigrator;

namespace AluguelIdeal.Infrastructure.Database.Migrations
{
    [MigrationTimestampVersion(
        description: "Initial Model Creation",
        transactionBehavior: TransactionBehavior.Default,
        year: 2021, month: 5, day: 16,
        hour: 21, minute: 4, second: 22)]
    [Tags(TagBehavior.RequireAny, "Testing", "Development", "Staging", "Production")]
    public class AddInitialModelMigration : Migration
    {
        public override void Up()
        {
            CreateUserTable();
            CreateRoleTable();
            CreateProfileTable();
            CreateCityTable();
            CreateDistrictTable();
            CreateResidenceTable();
            CreateLandlordTable();
            CreateImageTable();
            CreateHouseTable();
            CreateFlatTable();
        }

        public override void Down()
        {
            DropFlatTable();
            DropHouseTable();
            DropImageTable();
            DropLandlordTable();
            DropResidenceTable();
            DropDistrictTable();
            DropCityTable();
            DropProfileTable();
            DropRoleTable();
            DropUserTable();
        }

        private void CreateUserTable() =>
            Create.Table(nameof(User).ToLower())
            .WithColumn(nameof(User.Id)).AsGuid().PrimaryKey()
            .WithColumn(nameof(User.Name)).AsString(255).NotNullable()
            .WithColumn(nameof(User.Email)).AsString(255).NotNullable()
            .WithColumn(nameof(User.Phone)).AsString(20).Nullable()
            .WithColumn(nameof(User.Password)).AsString(255).Nullable()
            .WithColumn(nameof(User.DeletedAt)).AsDateTime().Nullable();

        private void DropUserTable() =>
            Delete.Table(nameof(User).ToLower());

        private void CreateRoleTable() =>
            Create.Table(nameof(Role).ToLower())
            .WithColumn(nameof(Role.Id)).AsGuid().PrimaryKey()
            .WithColumn(nameof(Role.Name)).AsString(255).NotNullable();

        private void DropRoleTable() =>
            Delete.Table(nameof(Role).ToLower());

        private void CreateProfileTable()
        {
            Create.Table(nameof(Profile).ToLower())
            .WithColumn(nameof(Profile.UserId)).AsGuid().ForeignKey(nameof(User).ToLower(), nameof(User.Id)).NotNullable()
            .WithColumn(nameof(Profile.RoleId)).AsGuid().ForeignKey(nameof(Role).ToLower(), nameof(Role.Id)).NotNullable();

            Create.PrimaryKey()
            .OnTable(nameof(Profile).ToLower())
            .Columns(new string[] { nameof(Profile.UserId), nameof(Profile.RoleId) });
            
        }

        private void DropProfileTable() =>
            Delete.Table(nameof(Profile).ToLower());

        private void CreateCityTable() =>
            Create.Table(nameof(City).ToLower())
            .WithColumn(nameof(City.Id)).AsGuid().PrimaryKey()
            .WithColumn(nameof(City.Name)).AsString(255).NotNullable();

        private void DropCityTable() =>
            Delete.Table(nameof(City).ToLower());

        private void CreateDistrictTable() =>
            Create.Table(nameof(District).ToLower())
            .WithColumn(nameof(District.Id)).AsGuid().PrimaryKey()
            .WithColumn(nameof(District.CityId)).AsGuid().ForeignKey(nameof(City).ToLower(), nameof(City.Id)).NotNullable()
            .WithColumn(nameof(District.Name)).AsString(255).NotNullable();

        private void DropDistrictTable() =>
            Delete.Table(nameof(District).ToLower());

        private void CreateResidenceTable() =>
            Create.Table(nameof(Residence).ToLower())
            .WithColumn(nameof(Residence.Id)).AsGuid().PrimaryKey()
            .WithColumn(nameof(Residence.DistrictId)).AsGuid().ForeignKey(nameof(District).ToLower(), nameof(District.Id)).NotNullable()
            .WithColumn(nameof(Residence.Street)).AsString(255).NotNullable()
            .WithColumn(nameof(Residence.Bedrooms)).AsInt32().NotNullable()
            .WithColumn(nameof(Residence.Bathrooms)).AsInt32().NotNullable()
            .WithColumn(nameof(Residence.Area)).AsDouble().NotNullable()
            .WithColumn(nameof(Residence.Rent)).AsDouble().NotNullable()
            .WithColumn(nameof(Residence.Tax)).AsDouble().NotNullable()
            .WithColumn(nameof(Residence.Description)).AsString().Nullable()
            .WithColumn(nameof(Residence.DeletedAt)).AsDateTime().Nullable();

        private void DropResidenceTable() =>
            Delete.Table(nameof(Residence).ToLower());

        private void CreateLandlordTable()
        {
            Create.Table(nameof(Landlord).ToLower())
            .WithColumn(nameof(Landlord.UserId)).AsGuid().ForeignKey(nameof(User).ToLower(), nameof(User.Id)).NotNullable()
            .WithColumn(nameof(Landlord.ResidenceId)).AsGuid().ForeignKey(nameof(Residence).ToLower(), nameof(Residence.Id)).NotNullable();

            Create.PrimaryKey()
            .OnTable(nameof(Landlord).ToLower())
            .Columns(new string[] { nameof(Landlord.UserId), nameof(Landlord.ResidenceId) });
        }

        private void DropLandlordTable() =>
            Delete.Table(nameof(Landlord).ToLower());

        private void CreateImageTable() =>
            Create.Table(nameof(Image).ToLower())
            .WithColumn(nameof(Image.Id)).AsGuid().PrimaryKey()
            .WithColumn(nameof(Image.ResidenceId)).AsGuid().ForeignKey(nameof(Residence).ToLower(), nameof(Residence.Id)).NotNullable()
            .WithColumn(nameof(Image.Path)).AsString(255).NotNullable();

        private void DropImageTable() =>
            Delete.Table(nameof(Image).ToLower());

        private void CreateHouseTable() =>
            Create.Table(nameof(House).ToLower())
            .WithColumn(nameof(House.Id)).AsGuid().PrimaryKey()
            .WithColumn("residence_id").AsGuid().ForeignKey(nameof(Residence).ToLower(), nameof(Residence.Id)).NotNullable()
            .WithColumn(nameof(House.YardArea)).AsDouble().NotNullable();

        private void DropHouseTable() =>
            Delete.Table(nameof(House).ToLower());

        private void CreateFlatTable() =>
            Create.Table(nameof(Flat).ToLower())
            .WithColumn(nameof(Flat.Id)).AsGuid().PrimaryKey()
            .WithColumn("residence_id").AsGuid().ForeignKey(nameof(Residence).ToLower(), nameof(Residence.Id)).NotNullable()
            .WithColumn(nameof(Flat.Condominium)).AsDouble().NotNullable()
            .WithColumn(nameof(Flat.Floor)).AsInt32().NotNullable();

        private void DropFlatTable() =>
            Delete.Table(nameof(Flat).ToLower());
    }
}
