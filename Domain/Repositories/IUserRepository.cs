using Domain.Models.Entities.LocalUser;

namespace Domain.Repositories;

public interface IUserRepository : IRepository<LocalUser>
{
    public Task<bool> HasLogin( string login );
    public Task<LocalUser?> GetUser( string login );
}
