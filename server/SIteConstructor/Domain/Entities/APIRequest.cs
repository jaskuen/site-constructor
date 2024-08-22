using System.Net;
using SiteConstructor.Dto;

namespace SiteConstructor.Domain.Entities;

public class APIRequest
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public List<string>? ErrorMessages { get; set; }
    public LoginResponseDTO? Result { get; set; }
    public APIRequest()
    {
        ErrorMessages = new List<string>();
    }
}
