using Domain.Models.Entities.LocalUser;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<LocalUser>, IUserRepository
{
    protected UserRepository( DbSet<LocalUser> entities )
        : base( entities )
    {
    }

    public UserRepository( ApplicationDbContext dbContext )
        : base( dbContext )
    {
    }

    public Task<bool> HasLogin( string login )
    {
        return Table.AnyAsync( x => x.Username == login );
    }

    public Task<LocalUser?> GetUser( string login )
    {
        return Table.FirstOrDefaultAsync( x => x.Username == login );
    }
}