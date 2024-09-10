using System.IO.Compression;
using System.Net.Mime;
using System.Text.Json;
using Domain.Models.ValueObjects.SiteData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("test")]
    [Authorize]
    public IActionResult Test()
    {
        return Ok();
    }

    [Authorize]
    [HttpPost("GetData")]
    public IActionResult GetData([FromBody] SiteData siteData)
    {
        if (siteData == null)
        {
            throw new ArgumentNullException("Site data is null");
        }
        _siteRepository.CreateHugoDirectory("");
        _siteRepository.CreatePhotoFiles(siteData);
        _siteRepository.SetOrUpdateData(siteData);
        string jsonPath = "./site-creator/static/data/data.json";
        System.IO.File.WriteAllText(jsonPath, JsonSerializer.Serialize(siteData));
        _siteRepository.GetSiteData();
        return Ok();
    }

    [Authorize]
    [HttpGet("DownloadResultSite")]
    public IActionResult DownloadResultSite()
    {
        SiteDataService.BuildHugoSite("./site-creator");
        string folderPath = Path.Combine("./site-creator/public");
        string zipFileName = "result.zip";

        string tempZipFilePath = Path.Combine(Path.GetTempPath(), zipFileName);

        if (System.IO.File.Exists(tempZipFilePath))
        {
            System.IO.File.Delete(tempZipFilePath);
        }

        ZipFile.CreateFromDirectory(folderPath, tempZipFilePath);

        var memory = new MemoryStream();
        using (var stream = new FileStream(tempZipFilePath, FileMode.Open))
        {
            stream.CopyTo(memory);
        }

        memory.Position = 0;

        System.IO.File.Delete(tempZipFilePath);

        Response.Headers.Append("Content-Disposition", new ContentDisposition
        {
            FileName = "result.zip",
            Inline = false,
        }.ToString());

        return File(memory, "application/zip", zipFileName);
    }
}
