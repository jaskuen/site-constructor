using Domain.Models.Entities.UserSiteData;

namespace Domain.Repositories;

public interface IUserSiteDataRepository : IRepository<UserSiteData>
{
    public Task<UserSiteData?> GetSiteData(int userId);
}
