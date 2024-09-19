using System.IO.Compression;
using System.Net.Mime;
using System.Text.Json;
using Application.UseCases.Commands.GetSiteData;
using Application.UseCases.Commands.GetSiteData.DTOs;
using Application.UseCases.Queries.DownloadResultSite;
using Application.UseCases.Queries.DownloadResultSite.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Domain.Models.ValueObjects.SiteData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteConstructor.Domain.Repositories;

namespace Application.Controllers;

[Route("api/SiteDataAPI")]
[ApiController]
public class SiteDataController : ControllerBase
{
    private readonly ICommandHandler<GetSiteDataCommand, Result> _getDataHandler;
    private readonly IQueryHandler<DownloadResultSiteQuery, DownloadResultSiteQueryResult> _downloadSiteHandler;
    public SiteDataController(
            ICommandHandler<GetSiteDataCommand, Result> getDataHandler,
            IQueryHandler<DownloadResultSiteQuery, DownloadResultSiteQueryResult> downloadSiteHandler
        )
    {
        _getDataHandler = getDataHandler;
        _downloadSiteHandler = downloadSiteHandler;
    }   

    [Authorize]
    [HttpPost("GetData")]
    public async Task<IActionResult> GetData([FromBody] GetSiteDataRequestDto model)
    {
        var result = await _getDataHandler.Handle( new GetSiteDataCommand(model) );
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize]
    [HttpGet("DownloadResultSite")]
    public async Task<IActionResult> DownloadResultSite([FromQuery] DownloadResultSiteRequestDto model)
    {
        var result = await _downloadSiteHandler.Handle(new DownloadResultSiteQuery(model));
        return result.IsSuccess ? File(result.Data.Memory.ToArray(), result.Data.ContentType, result.Data.FileName) : BadRequest(result);
    }
}
