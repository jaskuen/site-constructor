using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

