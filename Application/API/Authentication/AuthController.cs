using Application.Authentication.Results;
using Application.Dto;
using Application.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Application.Authentication;

[Route("api/UserAuth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserAuthenticationService _authService;

    public AuthController(IUserAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpOptions]
    public IActionResult Preflight()
    {
        return NoContent();
    }

    [HttpPost("checkLogin")]
    public bool CheckLogin([FromBody] CheckLoginDTO model)
    {
        return _authService.IsUniqueUser(model.Login);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
    {
        TokenDTO? loginResponse = await _authService.TryToLogin(model);
        if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
        {
            return BadRequest( LoginResult.Fail( new Error( "Username or password is incorrect" ) ) );
        }

        Response.Cookies.Append("tasty-cookies", loginResponse.Token, new CookieOptions
        {
            SameSite = SameSiteMode.Lax,
        });

        return Ok( LoginResult.Ok( loginResponse ) );
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
    {
        bool ifUserNameUnique = _authService.IsUniqueUser(model.Login);
        if (!ifUserNameUnique)
        {
            return BadRequest( Result.Fail( new Error( "Username already exists" ) ) );
        }

        var user = await _authService.TryToRegister(model);
        if (user == null)
        {
            return BadRequest( Result.Fail( new Error( "Error while registering" ) ) );
        }

        return Ok( Result.Ok() );
    }
}
