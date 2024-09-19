using Domain.Models.ValueObjects.SiteData;

namespace SiteConstructor.Domain.Repositories;

public interface ISiteDataRepository
{
    public SiteData GetSiteData();
    public void SetOrUpdateData(SiteData siteData);
    public void CreateHugoDirectory();
    public void ApplyDataToHugo();
}
