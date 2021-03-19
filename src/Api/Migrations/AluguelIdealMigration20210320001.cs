using System;
using FluentMigrator;


namespace AluguelIdeal.Api.Migrations
{
    [Migration(2021_03_20_001)]
    public class AluguelIdealMigration20210320001 : Migration
    {
        public override void Up()
        {
            Execute.Sql("DROP TABLE IF EXISTS \"Advertisement\"");
            Execute.Sql("DROP TABLE IF EXISTS \"Contact\"");

            Create.Table("\"Advertisement\"")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("title").AsString(255).NotNullable()
                .WithColumn("deleted_at").AsDateTime().Nullable();

            Create.Table("\"Contact\"")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("name").AsString(255).NotNullable()
                .WithColumn("deleted_at").AsDateTime().Nullable();

            InsertAdvertisement(1, "Lorem ipsum dolor sit amet");
            InsertAdvertisement(2, "Mauris vel lorem ac nisi ultricies euismod");
            InsertAdvertisement(3, "Morbi mauris metus, faucibus efficitur ante at, pharetra tincidunt ex");

            InsertContact(1, "Aliquam aliquet nisi eget varius viverra");
            InsertContact(2, "Morbi blandit sit amet massa");
            InsertContact(3, "Nunc sit amet ullamcorper odio ac pulvinar ex");
        }


        public override void Down()
        {
            Delete.Table("\"Advertisement\"");
            Delete.Table("\"Contact\"");
        }
        private void InsertAdvertisement(int id, string title, DateTime? deleted_at = null)
        {
            Insert.IntoTable("\"Advertisement\"").Row(new
            {
                id,
                title,
                deleted_at
            });
        }

        private void InsertContact(int id, string name, DateTime? deleted_at = null)
        {
            Insert.IntoTable("\"Contact\"").Row(new
            {
                id,
                name,
                deleted_at
            });
        }

    }
}
