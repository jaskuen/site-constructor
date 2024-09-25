using Application.UseCases.Authentication.Commands.Register;
using Application.UseCases.Authentication.Commands.Register.DTOs;
using Application.UseCases.Authentication.Queries.CheckLogin;
using Application.UseCases.Authentication.Queries.CheckLogin.DTOs;
using Application.UseCases.Authentication.Queries.Login;
using Application.UseCases.Authentication.Queries.Login.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Application.Authentication;

[Route("api/UserAuth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IQueryHandler<LoginQuery, LoginQueryResult> _loginHandler;
    private readonly IQueryHandler<CheckLoginQuery, CheckLoginQueryResult> _checkLoginHandler;
    private readonly ICommandHandler<RegisterCommand, Result> _registerHandler;

    public AuthController(
        IQueryHandler<LoginQuery, LoginQueryResult> loginHandler,
        IQueryHandler<CheckLoginQuery, CheckLoginQueryResult> checkLoginHandler,
        ICommandHandler<RegisterCommand, Result> registerHandler)
    {
        _loginHandler = loginHandler;
        _checkLoginHandler = checkLoginHandler;
        _registerHandler = registerHandler;
    }

    [HttpOptions]
    public IActionResult Preflight()
    {
        return NoContent();
    }

    [HttpPost("checkLogin")]
    public async Task<IActionResult> CheckLogin([FromBody] CheckLoginRequestDto model)
    {
        var result = await _checkLoginHandler.Handle(new CheckLoginQuery(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var result = await _loginHandler.Handle(new LoginQuery(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
    {
        var result = await _registerHandler.Handle(new RegisterCommand(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
