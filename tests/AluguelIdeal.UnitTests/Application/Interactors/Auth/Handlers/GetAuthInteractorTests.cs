using AluguelIdeal.Application.Interactors.Auth.Handlers;
using AluguelIdeal.Application.Interactors.Auth.Queries;
using AluguelIdeal.Application.Services;
using Moq;
using Moq.AutoMock;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AluguelIdeal.UnitTests.Application.Interactors.Auth.Handlers
{
    public class GetAuthInteractorTests
    {
        [Fact(DisplayName = "When GetAuthInteractor.Handle is called, it should call AuthService.CreateBearerTokenAsync")]
        public async Task Handle_Calls_CreateBearerTokenAsync()
        {
            // Arrange
            GetAuthQuery request = new GetAuthQuery()
            {
                ClientId = "any_client_id",
                GrantType = "any_grant_type",
                Email = "any_email",
                Password = "any_password"
            };
            string anyAccessToken = "any_access_token";
            int anyExpiresIn = 8 * 3600;
            AutoMocker mocker = new AutoMocker(MockBehavior.Strict);
            mocker.GetMock<IAuthService>()
                .Setup(service => service.CreateBearerTokenAsync(request.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync((anyAccessToken, anyExpiresIn))
                .Verifiable();
            GetAuthInteractor sut = mocker.CreateInstance<GetAuthInteractor>();

            // Act
            await sut.Handle(request, default);

            // Assign
            mocker.GetMock<IAuthService>()
                .Verify(service => service.CreateBearerTokenAsync(request.Email, It.IsAny<CancellationToken>()), Times.Once, failMessage: "CreateBearerTokenAsync was not called once");
        }
    }
}
