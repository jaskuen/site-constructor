using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteConstructor.Domain.Entities;
using SiteConstructor.Domain.Repositories;

namespace SiteConstructor.Controllers;

[Route("api/SiteDataAPI")]
[ApiController]
public class SiteDataController : ControllerBase
{
    private readonly ISiteDataRepository _siteRepository;
    public SiteDataController(ISiteDataRepository siteRepository)
    {
        _siteRepository = siteRepository;
    }

    [HttpPost("GetData")]
    public IActionResult GetData([FromBody] SiteData siteData)
    {
        if (siteData == null)
        {
            throw new ArgumentNullException("Site data is null");
        }

        _siteRepository.SetOrUpdateData(siteData);
        string jsonPath = "./site-creator/static/data/data.json";
        System.IO.File.WriteAllText(jsonPath, JsonSerializer.Serialize(siteData));
        _siteRepository.GetSiteData();
        return Ok();
    }
}
