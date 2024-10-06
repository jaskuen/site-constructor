using Domain.Models.ValueObjects.SiteData;

namespace Application.UseCases.Commands.GetSiteData.DTOs;

public class SetResultSiteDataRequestDto
{
    public SiteData SiteData { get; set; }
}
