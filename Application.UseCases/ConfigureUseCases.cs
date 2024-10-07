using Application.UseCases.Commands.GetSiteData;
using Application.UseCases.Commands.HostResultSite;
using Application.UseCases.Commands.SaveUserSiteData;
using Application.UseCases.Queries.DownloadResultSite;
using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases;

public static class ConfigureUseCases
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IQueryHandler<CheckHostNameQuery, CheckHostNameQueryResult>, CheckHostNameQueryHandler>();
        services.AddScoped<IQueryHandler<DownloadResultSiteQuery, DownloadResultSiteQueryResult>, DownloadResultSiteQueryHandler>();
        services.AddScoped<ICommandHandler<SetResultSiteDataCommand, Result>, SetResultSiteDataCommandHandler>();
        services.AddScoped<ICommandHandler<HostResultSiteCommand, Result>, HostResultSiteCommandHandler>();
        return services;
    }
}
