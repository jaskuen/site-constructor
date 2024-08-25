using Microsoft.EntityFrameworkCore;
using SiteConstructor.Domain.Entities;

namespace SiteConstructor.Data
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
