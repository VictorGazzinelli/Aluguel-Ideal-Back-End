using Bogus;
using Bogus.Extensions;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using AdvertisementEntity = AluguelIdeal.Domain.Entities.Advertisement;
using ContactEntity = AluguelIdeal.Domain.Entities.Contact;

namespace AluguelIdeal.Infrastructure.Database.Migrations
{
    [Migration(1)]
    public class AluguelIdealDatabaseMigration : Migration
    {
        private static readonly string LOCALE_PT_BR = "pt_BR";

        public override void Up()
        {
            Execute.Sql("DROP TABLE IF EXISTS advertisement");
            Execute.Sql("DROP TABLE IF EXISTS contact");

            Create.Table("advertisement")
                .WithColumn("id").AsInt32().PrimaryKey("PK_advertisement").Identity()
                .WithColumn("title").AsString(255).NotNullable()
                .WithColumn("deleted_at").AsDateTime().Nullable();

            Create.Table("contact")
                .WithColumn("id").AsInt32().PrimaryKey("PK_contact").Identity()
                .WithColumn("name").AsString(255).NotNullable()
                .WithColumn("email").AsString(255).NotNullable()
                .WithColumn("phone").AsString(255).NotNullable()
                .WithColumn("deleted_at").AsDateTime().Nullable();

            foreach (object advertisement in GenerateFakeAdvertisements())
                Insert.IntoTable("advertisement").Row(advertisement);

            foreach (object contact in GenerateFakeContacts())
                Insert.IntoTable("contact").Row(contact);
        }
        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS advertisement");
            Execute.Sql("DROP TABLE IF EXISTS contact");
        }

        private static IEnumerable<object> GenerateFakeContacts()
        {
            return new Faker<ContactEntity>(LOCALE_PT_BR)
                .RuleFor(c => c.Name, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.ExampleEmail())
                .RuleFor(c => c.Phone, GetBrazilianPhone())
                .RuleFor(c => c.DeletedAt, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now).OrNull(f, .9f))
                .Generate(10)
                .Select(c => new { name = c.Name, email = c.Email, phone = c.Phone, deleted_at = c.DeletedAt });
        }

        private static Func<Faker, string> GetBrazilianPhone()
        {
            return f =>
            {
                f.Phone.Locale = "pt_BR";
                return f.Phone.PhoneNumberFormat(1)
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("-", "")
                    .Replace(" ", "");
            };
        }

        private static IEnumerable<object> GenerateFakeAdvertisements()
        {
            return new Faker<AdvertisementEntity>(LOCALE_PT_BR)
                .RuleFor(a => a.Title, f => Truncate(f.Lorem.Sentence(), 255))
                .RuleFor(a => a.DeletedAt, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now).OrNull(f, .9f))
                .Generate(10)
                .Select(a => new { title = a.Title, deleted_at = a.DeletedAt });
        }

        public static string Truncate( string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

    }
}
