using Domain.Models.Entities.LocalUser;
using Domain.Models.Entities.UserSiteData;
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
        public DbSet<UserSiteData> UsersSiteData { get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserSiteData>()
            //    .HasMany(d => d.PhotosSrc)
            //    .WithOne(p => p.UserSiteData)
            //    .HasForeignKey(p => p.UserSiteDataId);
            modelBuilder.Entity<UserSiteData>()
                .HasMany(d => d.Images)
                .WithOne(f => f.UserSiteData)
                .HasForeignKey(f => f.UserSiteDataId);
        }
    }
}
