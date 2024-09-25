using Application.UseCases.Authentication.Queries.CheckLogin.DTOs;
using Application.UseCases.UseCases;

namespace Application.UseCases.Authentication.Queries.CheckLogin;

public class CheckLoginQuery : IQuery<CheckLoginQueryResult>
{
    public readonly CheckLoginRequestDto CheckLoginRequestDto;

    public CheckLoginQuery(CheckLoginRequestDto checkLoginRequestDto)
    {
        CheckLoginRequestDto = checkLoginRequestDto;
    }
}