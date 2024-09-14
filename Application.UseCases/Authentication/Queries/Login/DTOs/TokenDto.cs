namespace Application.UseCases.Authentication.Queries.Login.DTOs;

public class TokenDto
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpireDate { get; set; }
}
