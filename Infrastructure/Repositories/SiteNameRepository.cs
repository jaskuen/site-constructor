using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Entities.SiteName;
using Domain.Models.Entities.UserSiteData;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SiteNameRepository : BaseRepository<SiteName>, ISiteNameRepository
{
    protected SiteNameRepository(DbSet<SiteName> entities)
        : base(entities)
    {
    }

    public SiteNameRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<bool> HasSiteWithName(string siteName)
    {
        return Table.AnyAsync(x => x.Name == siteName);
    }

    public Task<SiteName?> GetSiteName(int userId)
    {
        return Table.FirstOrDefaultAsync(x => x.UserId == userId);
    }
}
