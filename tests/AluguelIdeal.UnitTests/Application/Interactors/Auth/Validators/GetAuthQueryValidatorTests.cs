using AluguelIdeal.Application.Exceptions;
using AluguelIdeal.Application.Interactors.Auth.Queries;
using AluguelIdeal.Application.Interactors.Auth.Validators;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Application.Services;
using AluguelIdeal.Domain.Entities;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Auth.Validators
{
    public class GetAuthQueryValidatorTests
    {
        [Theory(DisplayName = "given invalid GetAuthQuery, When GetAuthQueryValidate is called, it should throw AuthErrorException")]
        [InlineData("not_password", "client_id", "user@mail.com", "123456")]
        [InlineData("password", "not_client_id", "user@mail.com", "123456")]
        [InlineData("password", "client_id", "not_email", "123456")]
        [InlineData("password", "client_id", "user@mail.com", "")]
        [InlineData("password", "client_id", "", "")]
        [InlineData("password", "client_id", null, null)]
        public void Invalid_Validate_ThorwsAuthErrorException(string grantType, string clientId, string email, string password)
        {
            // Assign
            GetAuthQuery invalidQuery = new GetAuthQuery()
            {
                ClientId = clientId,
                GrantType = grantType,
                Email = email,
                Password = password,
            };
            User user = new User()
            {
                Email = invalidQuery.Email,
                Password = invalidQuery.Password
            };
            AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
            mocker.GetMock<IUserRepository>()
                .Setup(repo => repo.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            mocker.GetMock<IHashingService>()
                .Setup(service => service.HashAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(string.Empty);
            GetAuthQueryValidator validator = mocker.CreateInstance<GetAuthQueryValidator>();

            // Act
            Action validation = () => validator.Validate(invalidQuery);

            // Assert
            validation.Should().Throw<AuthErrorException>();
        }
    }
}
