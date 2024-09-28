using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Entities.UserSiteData;

namespace Domain.Repositories;

public interface IUserSiteDataRepository : IRepository<UserSiteData>
{
    public Task<UserSiteData?> GetSiteData(int userId);
}
