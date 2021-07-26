using AluguelIdeal.Application.Dtos.Residences;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using AluguelIdeal.Infrastructure.Database.Migrations.Testing;
using FluentAssertions;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.IntegrationTests.WebTests.Controllers
{
    public class ResidencesTests : IntegrationTestBase
    {
        private readonly string _requestUri = "api/residences";

        public ResidencesTests(CustomWebApplicationFactory fixture) : base(fixture)
        {

        }

        [Fact(DisplayName = "GET api/residences should return all Residences in database")]
        public async Task Should_ReturnAllResidences()
        {
            //Assign
            Guid id = Guid.Parse("668c5b37-f50a-4684-bf09-73d5044ae369");

            // Act
            Flat flat = await GetService<IFlatRepository>().GetByIdAsync(id);

            // Assert
            flat.Should().NotBeNull();
            flat.Id.Should().Be(id);
            flat.Floor.Should().Be(10);
            flat.Condominium.Should().Be(500);
        }
    }
}
