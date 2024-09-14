namespace Application.UseCases.Authentication.Queries.CheckLogin.DTOs;

public class CheckLoginResponseDto
{
    public bool Exists { get; set; }

    public CheckLoginResponseDto( bool exists )
    {
        Exists = exists;
    }
}