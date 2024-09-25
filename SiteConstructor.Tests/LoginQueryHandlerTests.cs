using Application.UseCases.Authentication;
using Application.UseCases.Authentication.Queries.Login;
using Application.UseCases.Authentication.Queries.Login.DTOs;
using Domain.Repositories;

namespace SiteConstructor.Tests
{
    public class LoginQueryHandlerTests
    {
        private LoginQueryHandler _loginHandler;
        private IUserRepository _userRepository;
        private AuthConfiguration _authConfiguration;

        [SetUp]
        public void Setup()
        {
            _authConfiguration = new AuthConfiguration();
            _loginHandler = new LoginQueryHandler(_userRepository, _authConfiguration);
        }

        [Test]
        public void Handle_EmptyUser_ReturnsFailedResult()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto();

            // Act
            var result = _loginHandler.Handle(new LoginQuery(loginRequestDto));

            // Assert
            Assert.IsNotNull(result.Result.Error);
        }

        [Test]
        public void Handle_EmptyLogin_ReturnsFailedResult()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto()
            {
                Login = "",
                Password = "password",
            };

            // Act
            var result = _loginHandler.Handle(new LoginQuery(loginRequestDto));

            // Assert
            Assert.IsNotNull(result.Result.Error);
        }

        [Test]
        public void Handle_EmptyPassword_ReturnsFailedResult()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto()
            {
                Login = "",
                Password = "password",
            };

            // Act
            var result = _loginHandler.Handle(new LoginQuery(loginRequestDto));

            // Assert
            Assert.IsNotNull(result.Result.Error);
        }
    }
}