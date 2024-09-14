using Domain.Models.Entities.LocalUser;

namespace SiteConstructor.Dto;

public class LoginResponseDTO
{
    public LocalUser User { get; set; }
    public string Token { get; set; }
}
