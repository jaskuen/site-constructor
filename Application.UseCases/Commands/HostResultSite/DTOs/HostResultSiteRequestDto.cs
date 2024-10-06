using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Entities.SiteName;
using Domain.Models.Entities.UserSiteData;
using Domain.Models.ValueObjects.SiteData;

namespace Application.UseCases.Commands.SaveUserSiteData.DTOs;

public class HostResultSiteRequestDto
{
    public string Name { get; set; }
    public int UserId { get; set; }
}
