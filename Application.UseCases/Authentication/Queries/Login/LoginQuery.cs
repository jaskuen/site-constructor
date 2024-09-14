using Application.UseCases.Authentication.Queries.Login.DTOs;
using Application.UseCases.UseCases;

namespace Application.UseCases.Authentication.Queries.Login;

public class LoginQuery : IQuery<LoginQueryResult>
{
    public readonly LoginRequestDto LoginDto;

    public LoginQuery( LoginRequestDto loginDto )
    {
        LoginDto = loginDto;
    }
}