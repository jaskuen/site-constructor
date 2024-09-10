using Domain.Models.ValueObjects.SiteData;

namespace SiteConstructor.Domain.Repositories;

public interface ISiteDataRepository
{
    public SiteData GetSiteData();
    public void CreateHugoDirectory(string userId);
    public void SetOrUpdateData(SiteData siteData);
    public void CreatePhotoFiles(SiteData siteData);
}
