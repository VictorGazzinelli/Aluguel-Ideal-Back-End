using AluguelIdeal.Application.Dtos.Users;
using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Interactors.Users.Commands;
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
    public class UsersTests : IntegrationTestBase
    {
        private readonly string _requestUri = "api/users";

        public UsersTests(CustomWebApplicationFactory fixture) : base(fixture)
        {

        }

        [Fact(DisplayName = "GET api/users should return all Users in database")]
        public async Task Should_ReturnAllUsers()
        {
            // Assign
            IMapper mapper = GetService<IMapper>();
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            QueryResult<InsensitiveUserDto> expectedResult = new QueryResult<InsensitiveUserDto>()
            {
                Items = mapper.Map<IEnumerable<User>, IEnumerable<InsensitiveUserDto>>(SeedForTestingMaintenance.Users)
            };

            // Act
            (HttpStatusCode obtainedStatusCode, QueryResult<InsensitiveUserDto> obtainedResult) = await DoGetRequest<QueryResult<InsensitiveUserDto>>(_requestUri, userEmail: "admin@mail.com");

            // Assert
            obtainedStatusCode.Should().Be(expectedStatusCode);
            obtainedResult.Should().Be(expectedResult);
        }

        [Fact(DisplayName = "GET api/users/id should return user with given id in database")]
        public async Task Should_ReturnExistingResidence()
        {
            // Assign
            IMapper mapper = GetService<IMapper>();
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            InsensitiveUserDto expectedResult = mapper.Map<User, InsensitiveUserDto>(SeedForTestingMaintenance.Users.First());

            // Act
            (HttpStatusCode obtainedStatusCode, InsensitiveUserDto obtainedResult) = await DoGetRequest<InsensitiveUserDto>(_requestUri + $"/{expectedResult.Id}", userEmail: "admin@mail.com");

            // Assert
            obtainedStatusCode.Should().Be(expectedStatusCode);
            obtainedResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact(DisplayName = "POST api/users should create a new User")]
        [Priority(1)]
        public async Task Should_CreateNewUser()
        {
            // Assign
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            CreateUserCommand requestParams = new CreateUserCommand()
            {
                Name = "Another User",
                Email = "anotherUser@mail.com"
            };
            IUserRepository userRepository = GetService<IUserRepository>();
            IMapper mapper = GetService<IMapper>();
            User user = mapper.Map<User>(requestParams);

            // Act
            (HttpStatusCode obtainedStatusCode, IdResult obtainedResult) = await DoPostRequest<IdResult>(_requestUri, parameters: requestParams, userEmail: "admin@mail.com");

            // Assert
            obtainedStatusCode.Should().Be(expectedStatusCode);
            obtainedResult.Should().NotBeNull();
            obtainedResult.Id.Should().NotBeEmpty();
            userRepository.GetByIdAsync(obtainedResult.Id).Result.Should().BeEquivalentTo(user, config: opt => opt.Excluding(x => x.Id));
        }

        [Fact(DisplayName = "PUT api/users should update a existing User")]
        [Priority(1)]
        public async Task Should_UpdateExistingUser()
        {
            // Assign
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent;
            UpdateUserCommand requestParams = new UpdateUserCommand()
            {
                Id = Guid.Parse("c0a4fc36-ad34-4d4a-883d-81056e4118a7"),
                Name = "Any Name",
                Email = "anyUser@mail.com"
            };
            IUserRepository userRepository = GetService<IUserRepository>();
            IMapper mapper = GetService<IMapper>();
            User user = mapper.Map<User>(requestParams);

            // Act
            (HttpStatusCode obtainedStatusCode, JsonElement _) = await DoPutRequest(_requestUri + $"/{requestParams.Id}", parameters: requestParams, userEmail: "admin@mail.com");

            // Assert
            obtainedStatusCode.Should().Be(expectedStatusCode);
            userRepository.GetByIdAsync(requestParams.Id).Result.Should().BeEquivalentTo(user);
        }

        [Fact(DisplayName = "PUT api/users/id/register should register User")]
        [Priority(2)]
        public async Task Should_RegisterUser()
        {
            // Assign
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent;
            RegisterUserCommand requestParams = new RegisterUserCommand()
            {
                Id = Guid.Parse("c0a4fc36-ad34-4d4a-883d-81056e4118a7"),
                Name = "User2",
                Email = "user2@mail.com",
                Password = "123456",
                Phone = "999999999"
                
            };
            IUserRepository userRepository = GetService<IUserRepository>();
            IMapper mapper = GetService<IMapper>();
            User user = mapper.Map<User>(requestParams);

            // Act
            (HttpStatusCode obtainedStatusCode, JsonElement _) = await DoPutRequest(_requestUri + $"/{requestParams.Id}/register", parameters: requestParams, userEmail: "admin@mail.com");

            // Assert
            obtainedStatusCode.Should().Be(expectedStatusCode);
            var x = userRepository.GetByIdAsync(requestParams.Id).Result;
            x.Should().BeEquivalentTo(user, opt => opt.Excluding(u => u.Password));
        }

        [Fact(DisplayName = "DELETE api/users should delete a existing User")]
        [Priority(3)]
        public async Task Should_DeleteUser()
        {
            // Assign
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent;
            DeleteUserCommand requestParams = new DeleteUserCommand()
            {
                Id = Guid.Parse("c0a4fc36-ad34-4d4a-883d-81056e4118a7"),
            };
            IUserRepository userRepository = GetService<IUserRepository>();

            // Act
            (HttpStatusCode obtainedStatusCode, JsonElement _) = await DoDeleteRequest(_requestUri + $"/{requestParams.Id}", parameters: requestParams, userEmail: "admin@mail.com");

            // Assert
            obtainedStatusCode.Should().Be(expectedStatusCode);
            userRepository.GetByIdAsync(requestParams.Id).Result.DeletedAt.Should().NotBeNull();
        }
    }
}
