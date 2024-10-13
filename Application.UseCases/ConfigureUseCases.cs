using Application.UseCases.Content.Commands.HostResultSite;
using Application.UseCases.Content.Commands.SetResultSiteData;
using Application.UseCases.Content.Queries.CheckHostName;
using Application.UseCases.Content.Queries.DownloadResultSite;
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
