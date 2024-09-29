using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Entities.LocalUser;
using Domain.Models.Entities.UserSiteData;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserSiteDataRepository : BaseRepository<UserSiteData>, IUserSiteDataRepository
{
    protected UserSiteDataRepository(DbSet<UserSiteData> entities)
        : base(entities)
    {
    }

    public UserSiteDataRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<UserSiteData?> GetSiteData(int userId)
    {
        return Table
            .Include(d => d.BackgroundColors)
            .Include(d => d.TextColors)
            .Include(d => d.Images)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }
}
