using AluguelIdeal.Application.Dtos.Residences;
using AluguelIdeal.Application.Dtos.Residences.Flats;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using AluguelIdeal.Infrastructure.Database.Migrations.Testing;
using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
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
            // Assign
            IMapper mapper = GetService<IMapper>();
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            QueryResult<ResidenceDto> expectedResult = new QueryResult<ResidenceDto>()
            {
                Items = mapper.Map<IEnumerable<Residence>,IEnumerable<ResidenceDto>>(SeedForTestingMaintenance.Residences)
            };

            // Act
            (HttpStatusCode obtainedStatusCode, QueryResult<ResidenceDto> obtainedResult) = await DoGetRequest<QueryResult<ResidenceDto>>(_requestUri);

            // Assert
            obtainedResult.Should().Be(expectedResult);
            obtainedStatusCode.Should().Be(expectedStatusCode);
        }
    }
}
