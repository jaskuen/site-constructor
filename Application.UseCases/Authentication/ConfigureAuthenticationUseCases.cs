using Application.UseCases.Authentication.Commands.Register;
using Application.UseCases.Authentication.Queries.CheckLogin;
using Application.UseCases.Authentication.Queries.Login;
using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.Authentication;

public static class ConfigureAuthenticationUseCases
{
    public static IServiceCollection AddAuthenticationServices( 
        this IServiceCollection services,
        AuthConfiguration configuration )
    {
        services.AddSingleton( configuration );

        services.AddScoped<IQueryHandler<LoginQuery, LoginQueryResult>, LoginQueryHandler>();
        services.AddScoped<IQueryHandler<CheckLoginQuery, CheckLoginQueryResult>, CheckLoginQueryHandler>();
        services.AddScoped<ICommandHandler<RegisterCommand, Result>, RegisterCommandHandler>();

        return services;
    }  
}