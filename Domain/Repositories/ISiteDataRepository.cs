using Domain.Models.ValueObjects.SiteData;
using Domain.Repositories;

namespace SiteConstructor.Domain.Repositories;

public interface ISiteDataRepository : IRepository<SiteData>
{
    public Task<SiteData?> GetSiteData(int userId);
    public void SetOrUpdateData(SiteData siteData);
    public void SaveUserSiteData(SiteData siteData);
    public void CreateHugoDirectory();
    public void ApplyDataToHugo();
}
