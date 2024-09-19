using Application.UseCases.Authentication.Queries.Login.DTOs;
using Application.UseCases.Authentication.Queries.Login;
using Application.UseCases.UseCases;
using Domain.Repositories;
using Application.UseCases.Authentication;
using Moq;
using Domain.Models.Entities.LocalUser;

namespace SiteConstructor.Tests
{
    public class LoginQueryHandlerTests
    {
        private LoginQueryHandler _loginHandler;
        private Mock<IUserRepository> _userRepository;
        private Mock<AuthConfiguration> _authConfiguration;

        [SetUp]
        public void Setup()
        {
            _userRepository = new Mock<IUserRepository>();
            _authConfiguration = new Mock<AuthConfiguration>();
            _loginHandler = new LoginQueryHandler(_userRepository.Object, _authConfiguration.Object);
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

        [Test]
        public void Handle_WrongPassword_ReturnsFailedResult()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto()
            {
                Login = "user",
                Password = "password",
            };
            var user = new LocalUser()
            {
                Id = 1,
                Username = "user",
                Password = "12345678",
            };
            _userRepository.Setup(x => x.Add(user));

            // Act
            var result = _loginHandler.Handle(new LoginQuery(loginRequestDto));

            // Assert
            Assert.IsNotNull(result.Result.Error);
        }
    }
}