using Microsoft.AspNetCore.Mvc;
using SiteConstructor.Domain.Repositories;
using SiteConstructor.Dto;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Bot.Connector;
using System.Net;

namespace SiteConstructor.Authentication;

[Route("api/UserAuth")]
[ApiController]
public class AuthController : ControllerBase
{
  private readonly IUserAuthenticationService _authService;
  private APIRequestDTO _response;
  public AuthController(IUserAuthenticationService authService)
  {
    _authService = authService;
    _response = new();
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
  {
    var loginResponse = _authService.TryToLogin(model);
    if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
    {
      _response.StatusCode = HttpStatusCode.BadRequest;
      _response.IsSuccess = false;
      _response.ErrorMessages.Add("Username or password is incorrect");
      return BadRequest(_response);
    }
    _response.StatusCode = HttpStatusCode.OK;
    _response.IsSuccess = true;
    _response.Result = loginResponse;
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
