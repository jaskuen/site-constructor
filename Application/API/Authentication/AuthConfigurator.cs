using System.Text;
using Application.UseCases.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Application.Authentication;

public static class AuthConfigurator
{
    public static void AddAuthentication( IApplicationBuilder app )
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }

    public static void AddAuthenticationServices(
        IServiceCollection services,
        AuthConfiguration? configuration )
    {
        if ( configuration == null )
        {
            throw new ArgumentException( $"{nameof( AuthConfiguration )} was not specified" );
        }

        services.AddAuthenticationServices( configuration );
        
        services
           .AddAuthentication( x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            } )
           .AddJwtBearer( x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey( Encoding.ASCII.GetBytes( configuration.PrivateKey ) ),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[ "tasty-cookies" ];

                        return Task.CompletedTask;
                    },
                };
            } );

        services.AddAuthorization();
    }
}