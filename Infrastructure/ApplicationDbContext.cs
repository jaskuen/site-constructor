using Domain.Models.Entities.LocalUser;
using Domain.Models.Entities.SiteName;
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
        public DbSet<SiteName> SiteNames { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSiteData>()
                .HasMany(d => d.Images)
                .WithOne(f => f.UserSiteData)
                .HasForeignKey(f => f.UserSiteDataId);
            modelBuilder.Entity<UserSiteData>()
                .HasOne(d => d.BackgroundColors)
                .WithOne(c => c.UserSiteData)
                .HasForeignKey<BackgroundColors>(c => c.UserSiteDataId);
            modelBuilder.Entity<UserSiteData>()
                .HasOne(d => d.TextColors)
                .WithOne(c => c.UserSiteData)
                .HasForeignKey<TextColors>(c => c.UserSiteDataId);
            modelBuilder.Entity<TextColors>()
                .HasIndex(tc => tc.UserSiteDataId)
                .IsUnique();
        }
    }
}
