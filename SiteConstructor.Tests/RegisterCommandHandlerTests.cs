using Application.UseCases.Authentication.Queries.Login.DTOs;
using Application.UseCases.Authentication.Queries.Login;
using Application.UseCases.UseCases;
using Domain.Repositories;
using Application.UseCases.Authentication;
using Moq;
using Domain.Models.Entities.LocalUser;
using Microsoft.EntityFrameworkCore;
using Application.UseCases.Authentication.Queries.CheckLogin;
using Application.UseCases.Authentication.Queries.CheckLogin.DTOs;
using Application.UseCases.Authentication.Commands.Register;
using Application.UseCases.Authentication.Commands.Register.DTOs;
using System.Text.RegularExpressions;

namespace SiteConstructor.Tests
{
    public class RegisterCommandHandlerTests
    {
        private RegisterCommandHandler _registerHandler;
        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _registerHandler = new RegisterCommandHandler(_userRepository, _unitOfWork);
        }

        [Test]
        public void Handle_EmptyLogin_ReturnsFailedResult()
        {
            // Arrange
            var registerRequestDto = new RegistrationRequestDto() 
            {
                Login = "",
                Password = "password",
            };

            // Act
            var result = _registerHandler.Handle(new RegisterCommand(registerRequestDto));

            // Assert
            Assert.IsNotNull(result.Result.Error);
        }

        [Test]
        public void Handle_EmptyPassword_ReturnsFailedResult()
        {
            // Arrange
            var registerRequestDto = new RegistrationRequestDto()
            {
                Login = "username",
                Password = "",
            };

            // Act
            var result = _registerHandler.Handle(new RegisterCommand(registerRequestDto));

            // Assert
            Assert.IsNotNull(result.Result.Error);
        }
    }
}