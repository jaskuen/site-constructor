using Domain.Models.Entities.LocalUser;
using Domain.Models.ValueObjects.SiteData;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
