using Domain.Models.ValueObjects.SiteData;

namespace Application.UseCases.Content.Commands.SetResultSiteData.DTOs;

public class SetResultSiteDataRequestDto
{
    public SiteData SiteData { get; set; }
}
