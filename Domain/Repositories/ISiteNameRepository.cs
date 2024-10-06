using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Entities.SiteName;

namespace Domain.Repositories;

public interface ISiteNameRepository : IRepository<SiteName>
{
    public Task<bool> HasSiteWithName(string siteName);
    public Task<SiteName?> GetSiteName(int userId);
}
