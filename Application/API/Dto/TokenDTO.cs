namespace SiteConstructor.Dto;

public class TokenDTO
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpireDate { get; set; }
}
