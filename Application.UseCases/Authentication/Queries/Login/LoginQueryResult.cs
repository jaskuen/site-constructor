using Application.UseCases.Authentication.Queries.Login.DTOs;
using Application.UseCases.Results;

namespace Application.UseCases.Authentication.Queries.Login;

public class LoginQueryResult : Result<LoginQueryResult, TokenDto>
{
}