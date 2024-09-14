using Application.UseCases.Authentication.Queries.CheckLogin.DTOs;
using Application.UseCases.Results;

namespace Application.UseCases.Authentication.Queries.CheckLogin;

public class CheckLoginQueryResult : Result<CheckLoginQueryResult, CheckLoginResponseDto>
{
}