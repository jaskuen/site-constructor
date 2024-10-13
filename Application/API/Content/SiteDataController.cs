using Application.UseCases.Content.Commands.HostResultSite;
using Application.UseCases.Content.Commands.HostResultSite.DTOs;
using Application.UseCases.Content.Commands.SaveUserSiteData;
using Application.UseCases.Content.Commands.SaveUserSiteData.DTOs;
using Application.UseCases.Content.Commands.SetResultSiteData;
using Application.UseCases.Content.Commands.SetResultSiteData.DTOs;
using Application.UseCases.Content.Queries.CheckHostName;
using Application.UseCases.Content.Queries.CheckHostName.DTOs;
using Application.UseCases.Content.Queries.DownloadResultSite;
using Application.UseCases.Content.Queries.DownloadResultSite.DTOs;
using Application.UseCases.Content.Queries.GetSavedUserSiteData;
using Application.UseCases.Content.Queries.GetSavedUserSiteData.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Route("api/siteData")]
[ApiController]
public class SiteDataController : ControllerBase
{
    private readonly ICommandHandler<SetResultSiteDataCommand, Result> _setResultSiteDataHandler;
    private readonly ICommandHandler<SaveUserSiteDataCommand, Result> _saveUserSiteDataHandler;
    private readonly IQueryHandler<DownloadResultSiteQuery, DownloadResultSiteQueryResult> _downloadSiteHandler;
    private readonly IQueryHandler<GetSavedUserSiteDataQuery, GetSavedUserSiteDataQueryResult> _getSavedUserSiteDataHandler;
    private readonly ICommandHandler<HostResultSiteCommand, Result> _hostResultSiteHandler;
    private readonly IQueryHandler<CheckHostNameQuery, CheckHostNameQueryResult> _checkHostNameQueryHandler;
    public SiteDataController(
            ICommandHandler<SetResultSiteDataCommand, Result> setResultDataHandler,
            ICommandHandler<SaveUserSiteDataCommand, Result> saveUserSiteDataHandler,
            IQueryHandler<DownloadResultSiteQuery, DownloadResultSiteQueryResult> downloadSiteHandler,
            IQueryHandler<GetSavedUserSiteDataQuery, GetSavedUserSiteDataQueryResult> getSavedUserSiteDataHandler,
            ICommandHandler<HostResultSiteCommand, Result> hostResultSiteHandler,
            IQueryHandler<CheckHostNameQuery, CheckHostNameQueryResult> checkHostNameQueryHandler
        )
    {
        _setResultSiteDataHandler = setResultDataHandler;
        _saveUserSiteDataHandler = saveUserSiteDataHandler;
        _downloadSiteHandler = downloadSiteHandler;
        _getSavedUserSiteDataHandler = getSavedUserSiteDataHandler;
        _hostResultSiteHandler = hostResultSiteHandler;
        _checkHostNameQueryHandler = checkHostNameQueryHandler;
    }

    [Authorize]
    [HttpPost("post")]
    public async Task<IActionResult> PostResultSiteData([FromBody] SetResultSiteDataRequestDto model)
    {
        var result = await _setResultSiteDataHandler.Handle(new SetResultSiteDataCommand(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize]
    [HttpPost("save")]
    public async Task<IActionResult> SaveUserSiteData([FromBody] SaveUserSiteDataRequestDto model)
    {
        var result = await _saveUserSiteDataHandler.Handle(new SaveUserSiteDataCommand(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize]
    [HttpGet("load")]
    public async Task<IActionResult> GetSavedUserSiteData([FromQuery] GetSavedUserSiteDataRequetsDto model)
    {
        var result = await _getSavedUserSiteDataHandler.Handle(new GetSavedUserSiteDataQuery(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize]
    [HttpGet("download")]
    public async Task<IActionResult> DownloadResultSite([FromQuery] DownloadResultSiteRequestDto model)
    {
        var result = await _downloadSiteHandler.Handle(new DownloadResultSiteQuery(model));
        return result.IsSuccess ? File(result.Data.Memory.ToArray(), result.Data.ContentType, result.Data.FileName) : BadRequest(result);
    }

    [Authorize]
    [HttpPost("host")]
    public async Task<IActionResult> HostResultSite([FromBody] HostResultSiteRequestDto model)
    {
        var result = await _hostResultSiteHandler.Handle(new HostResultSiteCommand(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize]
    [HttpGet("checkHost")]
    public async Task<IActionResult> CheckHostName([FromQuery] CheckHostNameRequestDto model)
    {
        var result = await _checkHostNameQueryHandler.Handle(new CheckHostNameQuery(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
