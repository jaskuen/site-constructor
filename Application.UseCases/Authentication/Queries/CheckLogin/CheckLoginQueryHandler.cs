using Application.UseCases.Authentication.Queries.CheckLogin.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Domain.Repositories;

namespace Application.UseCases.Authentication.Queries.CheckLogin;

public class CheckLoginQueryHandler : IQueryHandler<CheckLoginQuery, CheckLoginQueryResult>
{
    private readonly IUserRepository _userRepository;

    public CheckLoginQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<CheckLoginQueryResult> Handle(CheckLoginQuery query)
    {
        if (query.CheckLoginRequestDto == null)
        {
            return CheckLoginQueryResult.Fail(new Error("No data provided"));
        }

        if (String.IsNullOrWhiteSpace(query.CheckLoginRequestDto.Login))
        {
            return CheckLoginQueryResult.Fail(new Error("Login is null"));
        }

        var doesLoginExist = await _userRepository.HasLogin(query.CheckLoginRequestDto.Login.Trim());

        return CheckLoginQueryResult.Ok(new CheckLoginResponseDto(doesLoginExist));
    }
}