using Application.UseCases.Authentication.Commands.Register.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;

namespace Application.UseCases.Authentication.Commands.Register;

public class RegisterCommand : ICommand<Result>
{
    public readonly RegistrationRequestDto RequestDto;

    public RegisterCommand( RegistrationRequestDto requestDto )
    {
        RequestDto = requestDto;
    }
}