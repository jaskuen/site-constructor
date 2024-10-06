using Domain.Models.Entities.UserSiteData;

namespace Application.UseCases.Commands.SaveUserSiteData.DTOs;

public class SaveUserSiteDataRequestDto
{
    public UserSiteData UserSiteData { get; set; }
}
