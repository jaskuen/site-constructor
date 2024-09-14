using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.UseCases.Authentication.Queries.Login.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Domain.Models.Entities.LocalUser;
using Domain.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Application.UseCases.Authentication.Queries.Login;

public class LoginQueryHandler : IQueryHandler<LoginQuery, LoginQueryResult>
{
    private readonly IUserRepository _userRepository;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    private readonly AuthConfiguration _authConfiguration;

    public LoginQueryHandler(
        IUserRepository userRepository,
        AuthConfiguration authConfiguration )
    {
        _userRepository = userRepository;
        _authConfiguration = authConfiguration;
    }

    public async Task<LoginQueryResult> Handle( LoginQuery query )
    {
        Error? validationError = Validate( query );
        if ( validationError != null )
        {
            return LoginQueryResult.Fail( validationError );
        }

        LocalUser? account = await _userRepository.GetUser( query.LoginDto.Login.Trim() );
        if ( account == null )
        {
            return LoginQueryResult.Fail( new Error( "Username or password is incorrect" ) );
        }

        if ( !string.Equals(
                account.Password,
                query.LoginDto.Password,
                StringComparison.InvariantCulture ) )
        {
            return LoginQueryResult.Fail( new Error( "Username or password is incorrect" ) );
        }

        var startDate = DateTime.UtcNow;
        var expireDate = startDate.AddMinutes( 10 );

        return LoginQueryResult.Ok( new TokenDto
        {
            UserId = account.Id,
            Token = _tokenHandler.WriteToken( BuildJwtProvider( startDate, expireDate ) ),
            ExpireDate = expireDate,
        } );
    }

    private Error? Validate( LoginQuery query )
    {
        if ( query.LoginDto == null )
        {
            return new Error( "No data provided" );
        }

        if ( String.IsNullOrWhiteSpace( query.LoginDto.Login ) )
        {
            return new Error( "Login is empty" );
        }

        if ( String.IsNullOrWhiteSpace( query.LoginDto.Password ) )
        {
            return new Error( "Password is empty" );
        }

        return null;
    }

    private SecurityToken BuildJwtProvider( DateTime startDate, DateTime expireDate )
    {
        var key = Encoding.ASCII.GetBytes( _authConfiguration.PrivateKey );

        var jwt = new JwtSecurityToken(
            expires: expireDate,
            notBefore: startDate,
            signingCredentials: new SigningCredentials( new SymmetricSecurityKey( key ), SecurityAlgorithms.HmacSha256 )
        );

        return jwt;
    }
}