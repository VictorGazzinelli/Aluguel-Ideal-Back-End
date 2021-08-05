using AluguelIdeal.Application.Dtos.Residences;
using AluguelIdeal.Application.Enums;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Interactors.Residences.Commands;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using AluguelIdeal.Infrastructure.Database.Migrations.Testing;
using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

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
                Items = mapper.Map<IEnumerable<Residence>, IEnumerable<ResidenceDto>>(SeedForTestingMaintenance.Residences)
            };

            // Act
            (HttpStatusCode obtainedStatusCode, QueryResult<ResidenceDto> obtainedResult) = await DoGetRequest<QueryResult<ResidenceDto>>(_requestUri);

            // Assert
            obtainedResult.Should().Be(expectedResult);
            obtainedStatusCode.Should().Be(expectedStatusCode);
        }

        [Fact(DisplayName = "GET api/residences/id should return residence with given id in database")]
        public async Task Should_ReturnExistingResidence()
        {
            // Assign
            IMapper mapper = GetService<IMapper>();
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            ResidenceDto expectedResult = mapper.Map<Residence, ResidenceDto>(SeedForTestingMaintenance.Residences.First());

            // Act
            (HttpStatusCode obtainedStatusCode, ResidenceDto obtainedResult) = await DoGetRequest<ResidenceDto>(_requestUri + $"/{expectedResult.Id}");

            // Assert
            obtainedStatusCode.Should().Be(expectedStatusCode);
            obtainedResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact(DisplayName = "POST api/residences should create a new Flat")]
        [Priority(1)]
        public async Task Should_CreateNewFlat()
        {
            // Assign
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            CreateResidenceCommand requestParams = new CreateResidenceCommand()
            {
                DistrictId = Guid.Parse("8f408fe8-b9fd-4be7-9c67-219bf97c50c2"),
                Street = "Any street",
                Bedrooms = 2,
                Bathrooms = 1,
                Area = 60,
                Rent = 2000,
                Tax = 300,
                Description = "Any Description",
                ResidenceType = ResidenceType.Flat,
                Condominium = 500,
                Floor = 3
            };
            IResidenceRepository residenceRepository = GetService<IResidenceRepository>();
            IMapper mapper = GetService<IMapper>();
            Residence residence = mapper.Map<Residence>(requestParams);

            // Act
            (HttpStatusCode obtainedStatusCode, IdResult obtainedResult) = await DoPostRequest<IdResult>(_requestUri, parameters: requestParams, userEmail: "landlord@mail.com");

            // Assert
            obtainedStatusCode.Should().Be(expectedStatusCode);
            obtainedResult.Should().NotBeNull();
            obtainedResult.Id.Should().NotBeEmpty();
            residence.Should().BeOfType<Flat>();
            residenceRepository.GetByIdAsync(obtainedResult.Id).Result.Should().BeEquivalentTo(residence, config: opt => opt.Excluding(x => x.Id));
        }

        [Fact(DisplayName = "POST api/residences should create a new House")]
        [Priority(1)]
        public async Task Should_CreateNewHouse()
        {
            // Assign
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            CreateResidenceCommand requestParams = new CreateResidenceCommand()
            {
                DistrictId = Guid.Parse("8f408fe8-b9fd-4be7-9c67-219bf97c50c2"),
                Street = "Any street",
                Bedrooms = 2,
                Bathrooms = 1,
                Area = 60,
                Rent = 2000,
                Tax = 300,
                Description = "Any Description",
                ResidenceType = ResidenceType.House,
                YardArea = 100
            };
            IResidenceRepository residenceRepository = GetService<IResidenceRepository>();
            IMapper mapper = GetService<IMapper>();
            Residence residence = mapper.Map<Residence>(requestParams);

            // Act
            (HttpStatusCode obtainedStatusCode, IdResult obtainedResult) = await DoPostRequest<IdResult>(_requestUri, parameters: requestParams, userEmail: "landlord@mail.com");

            // Assert
            obtainedStatusCode.Should().Be(expectedStatusCode);
            obtainedResult.Should().NotBeNull();
            obtainedResult.Id.Should().NotBeEmpty();
            residence.Should().BeOfType<House>();
            residenceRepository.GetByIdAsync(obtainedResult.Id).Result.Should().BeEquivalentTo(residence, config: opt => opt.Excluding(x => x.Id));
        }

        [Fact(DisplayName = "PUT api/residences should update a existing Residence")]
        [Priority(1)]
        public async Task Should_CreateUpdateResidence()
        {
            // Assign
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent;
            UpdateResidenceCommand requestParams = new UpdateResidenceCommand()
            {
                Id = Guid.Parse("668c5b37-f50a-4684-bf09-73d5044ae369"),
                DistrictId = Guid.Parse("8f408fe8-b9fd-4be7-9c67-219bf97c50c2"),
                Street = "Any street",
                Bedrooms = 2,
                Bathrooms = 1,
                Area = 60,
                Rent = 2000,
                Tax = 300,
                Description = "Any Description",
                ResidenceType = ResidenceType.Flat,
                Floor = 2,
                Condominium = 400
            };
            IResidenceRepository residenceRepository = GetService<IResidenceRepository>();
            IMapper mapper = GetService<IMapper>();
            Residence residence = mapper.Map<Residence>(requestParams);

            // Act
            (HttpStatusCode obtainedStatusCode, JsonElement _) = await DoPutRequest(_requestUri + $"/{requestParams.Id}", parameters: requestParams, userEmail: "landlord@mail.com");

            // Assert
            obtainedStatusCode.Should().Be(expectedStatusCode);
            residence.Should().BeOfType<Flat>();
            residenceRepository.GetByIdAsync(requestParams.Id).Result.Should().BeEquivalentTo(residence);
        }

        [Fact(DisplayName = "DELETE api/residences should delete a existing Residence")]
        [Priority(2)]
        public async Task Should_DeleteResidence()
        {
            // Assign
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent;
            DeleteResidenceCommand requestParams = new DeleteResidenceCommand()
            {
                Id = Guid.Parse("668c5b37-f50a-4684-bf09-73d5044ae369"),
            };
            IResidenceRepository residenceRepository = GetService<IResidenceRepository>();

            // Act
            (HttpStatusCode obtainedStatusCode, JsonElement _) = await DoDeleteRequest(_requestUri + $"/{requestParams.Id}", parameters: requestParams, userEmail: "landlord@mail.com");

            // Assert
            obtainedStatusCode.Should().Be(expectedStatusCode);
            residenceRepository.GetByIdAsync(requestParams.Id).Result.DeletedAt.Should().NotBeNull();
        }
    }
}