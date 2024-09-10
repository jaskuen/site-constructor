using System.Net;

namespace SiteConstructor.Dto;

public class APIRequestDTO
{
  public HttpStatusCode StatusCode { get; set; }
  public bool IsSuccess { get; set; }
  public List<string>? ErrorMessages { get; set; }
  public TokenDTO? Result { get; set; }
  public APIRequestDTO()
  {
    ErrorMessages = new List<string>();
  }
}
