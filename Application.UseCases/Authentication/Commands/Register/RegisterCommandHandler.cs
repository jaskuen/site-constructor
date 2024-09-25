using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Domain.Models.Entities.LocalUser;
using Domain.Repositories;

namespace Application.UseCases.Authentication.Commands.Register;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RegisterCommand command)
    {
        Error? validationError = Validate(command);
        if (validationError != null)
        {
            return Result.Fail(validationError);
        }

        string login = command.RequestDto.Login.Trim();
        string pass = command.RequestDto.Password.Trim();

        if (await _userRepository.HasLogin(login))
        {
            return Result.Fail(new Error("Username already exists"));
        }

        var user = new LocalUser
        {
            Username = login,
            Password = pass,
        };

        _userRepository.Add(user);
        _unitOfWork.Commit();

        return Result.Ok();
    }

    private Error? Validate(RegisterCommand command)
    {
        if (command.RequestDto == null)
        {
            return new Error("No data provided");
        }

        if (String.IsNullOrWhiteSpace(command.RequestDto.Login))
        {
            return new Error("Login is empty");
        }

        if (String.IsNullOrWhiteSpace(command.RequestDto.Password))
        {
            return new Error("Password is empty");
        }

        return null;
    }
}