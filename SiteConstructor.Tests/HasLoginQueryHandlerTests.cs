using Application.UseCases.Authentication.Queries.CheckLogin;
using Application.UseCases.Authentication.Queries.CheckLogin.DTOs;
using Domain.Repositories;

namespace SiteConstructor.Tests
{
    public class CheckLoginQueryHandlerTests
    {
        private CheckLoginQueryHandler _checkLoginHandler;
        private IUserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            _checkLoginHandler = new CheckLoginQueryHandler(_userRepository);
        }

        [Test]
        public void Handle_EmptyLogin_ReturnsFailedResult()
        {
            // Arrange
            var loginRequestDto = new CheckLoginRequestDto()
            {
                Login = "",
            };

            // Act
            var result = _checkLoginHandler.Handle(new CheckLoginQuery(loginRequestDto));

            // Assert
            Assert.IsNotNull(result.Result.Error);
        }
    }
}