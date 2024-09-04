using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Models.Entities.LocalUser;
using Domain.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using SiteConstructor.Dto;

namespace SiteConstructor.Authentication;

public interface IUserAuthenticationService
{
  /// <summary>
  /// Checks, if this login is free to use.
  /// </summary>
  /// <returns>If there is no user with that login, returns true, otherwise returns false</returns>
  bool IsUniqueUser(string username);
  /// <summary>
  /// Tryes to authenticate.
  /// </summary>
  /// <returns>If success, returns token, otherwise returns null</returns>
  Task<TokenDTO> TryToLogin(LoginRequestDTO request);
  /// <summary>
  /// Tryes to register user.
  /// </summary>
  /// <returns>If success, returns local user, otherwise returns null</returns>
  Task<LocalUser> TryToRegister(RegistrationRequestDTO request);
}

public class UserAuthenticationService : IUserAuthenticationService
{
  private readonly IUnitOfWork _unitOfWork;
  //private readonly AuthenticationOptions _options;
  private readonly IRepository<LocalUser> _userRepository;
  private readonly JwtSecurityTokenHandler _tokenHandler;

  private readonly string _key;

  public UserAuthenticationService(
      IRepository<LocalUser> userRepo,
      //AuthenticationOptions options,
      IConfiguration configuration,
      IUnitOfWork unitOfWork
    )
  {
    _userRepository = userRepo;
    //_options = options;
    _tokenHandler = new JwtSecurityTokenHandler();
    _unitOfWork = unitOfWork;
    _key = configuration.GetValue<string>("ApiSettings:Secret");
  }

  public async Task<LocalUser> TryToRegister(RegistrationRequestDTO request)
  {
    LocalUser user = new LocalUser()
    {
      Username = request.Login,
      Password = request.Password,
    };
    _userRepository.Add(user);
    _unitOfWork.Commit();
    user.Password = "";
    return user;
  }

  public async Task<TokenDTO> TryToLogin(LoginRequestDTO request)
  {
    var account = _userRepository.Table.FirstOrDefault(account => account.Username == request.Login);
    if (account == null)
    {
      return null;
    }

    if (!CheckPasswords(account.Password, request.Password))
    {
      return null;
    }

    DateTime startDate = DateTime.Now;
    DateTime expireDate = startDate.AddMinutes(10);
    var jwt = BuildJwtProvider(account, startDate, expireDate);

    var token = new TokenDTO()
    {
      Token = _tokenHandler.WriteToken(jwt),
      ExpireDate = expireDate,
    };

    //_unitOfWork.Commit();

    return token;
  }

  private static bool CheckPasswords(string presentPassword, string incomingPassword)
  {
    return presentPassword == incomingPassword;
  }

  public bool IsUniqueUser(string username)
  {
    var user = _userRepository.Table.FirstOrDefault(u => u.Username == username);
    if (user == null)
    {
      return true;
    }
    return false;
  }

  /// <summary>
  /// Used for building Jwt Provider that constructs security key
  /// </summary>
  private JwtSecurityToken BuildJwtProvider(LocalUser user, DateTime startDate, DateTime expireDate)
  {
    var key = Encoding.ASCII.GetBytes(_key);
    var credantials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

    var jwt = new JwtSecurityToken(
        expires: expireDate,
        notBefore: startDate,
        signingCredentials: credantials
        );

    return jwt;
  }

  private static ClaimsIdentity GetIdentity(LocalUser user)
  {
    var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username ),
        };

    return new ClaimsIdentity(claims, "Token");
  }
}
