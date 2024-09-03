using Microsoft.AspNetCore.Mvc;
using SiteConstructor.Dto;
using System.Net;

namespace SiteConstructor.Authentication;

[Route("api/UserAuth")]
[ApiController]
public class AuthController : ControllerBase
{
  private readonly IUserAuthenticationService _authService;
  private APIRequestDTO _response;
  private readonly HttpContext _context;
  public AuthController(IUserAuthenticationService authService, IHttpContextAccessor contextAccessor)
  {
    _authService = authService;
    _response = new();
    _context = contextAccessor.HttpContext;
  }

  [HttpOptions]
  public IActionResult Preflight()
  {
    return NoContent();
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
  {
    var loginResponse = await _authService.TryToLogin(model);
    if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
    {
      _response.StatusCode = HttpStatusCode.BadRequest;
      _response.IsSuccess = false;
      _response.ErrorMessages.Add("Username or password is incorrect");
      return BadRequest(_response);
    }
    _response.StatusCode = HttpStatusCode.OK;
    _response.IsSuccess = true;
    _context.Response.Cookies.Append("tasty-cookies", loginResponse.Token, new CookieOptions
      {
        SameSite = SameSiteMode.None,
        Secure = true
      });
    Console.WriteLine(loginResponse.Token);
    return Ok(_response);
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
  {
    bool ifUserNameUnique = _authService.IsUniqueUser(model.Login);
    if (!ifUserNameUnique)
    {
      _response.StatusCode = HttpStatusCode.BadRequest;
      _response.IsSuccess = false;
      _response.ErrorMessages.Add("Username already exists");
      return BadRequest(_response);
    }
    var user = await _authService.TryToRegister(model);
    if (user == null)
    {
      _response.StatusCode = HttpStatusCode.BadRequest;
      _response.IsSuccess = false;
      _response.ErrorMessages.Add("Error while registering");
      return BadRequest(_response);
    }

    _response.StatusCode = HttpStatusCode.OK;
    _response.IsSuccess = true;
    return Ok(_response);
  }
}
