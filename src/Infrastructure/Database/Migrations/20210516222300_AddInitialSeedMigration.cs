using AluguelIdeal.Domain.Entities;
using AluguelIdeal.Infrastructure.Database.Migrations.Attributes;
using FluentMigrator;
using System;

namespace AluguelIdeal.Infrastructure.Database.Migrations
{
    [MigrationTimestampVersion(
        description: "Initial Seed Creation",
        transactionBehavior: TransactionBehavior.Default,
        year: 2021, month: 5, day: 16,
        hour: 22, minute: 23, second: 00)]
    [Tags(TagBehavior.RequireAny, "Testing", "Development", "Staging")]
    public class AddInitialSeedMigration : Migration
    {
        public override void Up()
        {
            CreateRoles();
            CreateUsers();
            CreateProfiles();
        }

        public override void Down()
        {
            DeleteProfiles();
            DeleteRoles();
            DeleteUsers();
        }

        private void DeleteUsers()
        {
            Delete.FromTable(nameof(User).ToLower())
                .Row(new
                {
                    id = Guid.Parse("a298051c-b6af-11eb-8529-0242ac130003"),
                    name = "Admin",
                    email = "admin@mail.com",
                    password = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92"
                })
                .Row(new
                 {
                     id = Guid.Parse("cc5e0018-44be-4bd4-8045-556541eb0b2e"),
                     name = "Landlord",
                     email = "landlord@mail.com",
                     password = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92"
                 })
                .Row(new
                 {
                     id = Guid.Parse("92145c8e-5f38-471b-9ded-e03b6c0a0767"),
                     name = "User",
                     email = "user@mail.com",
                     password = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92"
                 });
        }

        private void DeleteRoles()
        {
            Delete.FromTable(nameof(Role).ToLower())
                            .Row(new { id = Guid.Parse("4cc1053a-b6af-11eb-8529-0242ac130003"), name = "Admin" })
                            .Row(new { id = Guid.Parse("4cc1092c-b6af-11eb-8529-0242ac130003"), name = "Landlord" })
                            .Row(new { id = Guid.Parse("4cc10b66-b6af-11eb-8529-0242ac130003"), name = "User" });
        }

        private void DeleteProfiles()
        {
            Delete.FromTable(nameof(Profile).ToLower())
                            .Row(new { user_id = Guid.Parse("a298051c-b6af-11eb-8529-0242ac130003"), role_id = Guid.Parse("4cc1053a-b6af-11eb-8529-0242ac130003") })
                            .Row(new { user_id = Guid.Parse("a298051c-b6af-11eb-8529-0242ac130003"), role_id = Guid.Parse("4cc1092c-b6af-11eb-8529-0242ac130003") })
                            .Row(new { user_id = Guid.Parse("a298051c-b6af-11eb-8529-0242ac130003"), role_id = Guid.Parse("4cc10b66-b6af-11eb-8529-0242ac130003") })
                            .Row(new { user_id = Guid.Parse("cc5e0018-44be-4bd4-8045-556541eb0b2e"), role_id = Guid.Parse("4cc1092c-b6af-11eb-8529-0242ac130003") })
                            .Row(new { user_id = Guid.Parse("cc5e0018-44be-4bd4-8045-556541eb0b2e"), role_id = Guid.Parse("4cc10b66-b6af-11eb-8529-0242ac130003") })
                            .Row(new { user_id = Guid.Parse("92145c8e-5f38-471b-9ded-e03b6c0a0767"), role_id = Guid.Parse("4cc10b66-b6af-11eb-8529-0242ac130003") });
        }

        private void CreateProfiles()
        {
            Insert.IntoTable(nameof(Profile).ToLower())
                            .Row(new { user_id = Guid.Parse("a298051c-b6af-11eb-8529-0242ac130003"), role_id = Guid.Parse("4cc1053a-b6af-11eb-8529-0242ac130003") })
                            .Row(new { user_id = Guid.Parse("a298051c-b6af-11eb-8529-0242ac130003"), role_id = Guid.Parse("4cc1092c-b6af-11eb-8529-0242ac130003") })
                            .Row(new { user_id = Guid.Parse("a298051c-b6af-11eb-8529-0242ac130003"), role_id = Guid.Parse("4cc10b66-b6af-11eb-8529-0242ac130003") })
                            .Row(new { user_id = Guid.Parse("cc5e0018-44be-4bd4-8045-556541eb0b2e"), role_id = Guid.Parse("4cc1092c-b6af-11eb-8529-0242ac130003") })
                            .Row(new { user_id = Guid.Parse("cc5e0018-44be-4bd4-8045-556541eb0b2e"), role_id = Guid.Parse("4cc10b66-b6af-11eb-8529-0242ac130003") })
                            .Row(new { user_id = Guid.Parse("92145c8e-5f38-471b-9ded-e03b6c0a0767"), role_id = Guid.Parse("4cc10b66-b6af-11eb-8529-0242ac130003") });
        }

        private void CreateUsers()
        {
            Insert.IntoTable(nameof(User).ToLower())
                .Row(new
                {
                    id = Guid.Parse("a298051c-b6af-11eb-8529-0242ac130003"),
                    name = "Admin",
                    email = "admin@mail.com",
                    password = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92"
                })
                .Row(new
                {
                    id = Guid.Parse("cc5e0018-44be-4bd4-8045-556541eb0b2e"),
                    name = "Landlord",
                    email = "landlord@mail.com",
                    password = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92"
                })
                .Row(new
                {
                    id = Guid.Parse("92145c8e-5f38-471b-9ded-e03b6c0a0767"),
                    name = "User",
                    email = "user@mail.com",
                    password = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92"
                });
        }

        private void CreateRoles()
        {
            Insert.IntoTable(nameof(Role).ToLower())
                            .Row(new { id = Guid.Parse("4cc1053a-b6af-11eb-8529-0242ac130003"), name = "Admin" })
                            .Row(new { id = Guid.Parse("4cc1092c-b6af-11eb-8529-0242ac130003"), name = "Landlord" })
                            .Row(new { id = Guid.Parse("4cc10b66-b6af-11eb-8529-0242ac130003"), name = "User" });
        }
    }
}
