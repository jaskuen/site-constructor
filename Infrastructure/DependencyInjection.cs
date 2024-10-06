using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiteConstructor.Domain.Repositories;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISiteDataRepository, SiteDataRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserSiteDataRepository, UserSiteDataRepository>();
        services.AddScoped<ISiteNameRepository, SiteNameRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
