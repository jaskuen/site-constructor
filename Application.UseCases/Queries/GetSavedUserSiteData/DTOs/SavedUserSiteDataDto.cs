using Domain.Models.Entities.UserSiteData;

namespace Application.UseCases.Queries.GetSavedUserSiteData.DTOs;

public class SavedUserSiteDataDto
{
    public UserSiteData SiteData { get; set; }
    public SavedUserSiteDataDto(UserSiteData siteData)
    {
        SiteData = siteData;
    }
}

