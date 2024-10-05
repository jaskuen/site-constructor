using Application.UseCases.Commands.GetSiteData;
using Application.UseCases.Commands.GetSiteData.DTOs;
using Application.UseCases.Commands.SaveUserSiteData;
using Application.UseCases.Commands.SaveUserSiteData.DTOs;
using Application.UseCases.Queries.DownloadResultSite;
using Application.UseCases.Queries.DownloadResultSite.DTOs;
using Application.UseCases.Queries.GetSavedUserSiteData.DTOs;
using Application.UseCases.Queries.UploadSavedUserSiteData;
using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Domain.Models.ValueObjects.GitHub;
using Domain.Models.ValueObjects.GitHub.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Route("api/SiteDataAPI")]
[ApiController]
public class SiteDataController : ControllerBase
{
    private readonly ICommandHandler<SetResultSiteDataCommand, Result> _setResultSiteDataHandler;
    private readonly ICommandHandler<SaveUserSiteDataCommand, Result> _saveUserSiteDataHandler;
    private readonly IQueryHandler<DownloadResultSiteQuery, DownloadResultSiteQueryResult> _downloadSiteHandler;
    private readonly IQueryHandler<GetSavedUserSiteDataQuery, GetSavedUserSiteDataQueryResult> _getSavedUserSiteDataHandler;
    public SiteDataController(
            ICommandHandler<SetResultSiteDataCommand, Result> setResultDataHandler,
            ICommandHandler<SaveUserSiteDataCommand, Result> saveUserSiteDataHandler,
            IQueryHandler<DownloadResultSiteQuery, DownloadResultSiteQueryResult> downloadSiteHandler,
            IQueryHandler<GetSavedUserSiteDataQuery, GetSavedUserSiteDataQueryResult> getSavedUserSiteDataHandler
        )
    {
        _setResultSiteDataHandler = setResultDataHandler;
        _saveUserSiteDataHandler = saveUserSiteDataHandler;
        _downloadSiteHandler = downloadSiteHandler;
        _getSavedUserSiteDataHandler = getSavedUserSiteDataHandler;
    }

    [Authorize]
    [HttpPost("PostResultSiteData")]
    public async Task<IActionResult> PostResultSiteData([FromBody] SetResultSiteDataRequestDto model)
    {
        var result = await _setResultSiteDataHandler.Handle(new SetResultSiteDataCommand(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize]
    [HttpPost("SaveUserSiteData")]
    public async Task<IActionResult> SaveUserSiteData([FromBody] SaveUserSiteDataRequestDto model)
    {
        var result = await _saveUserSiteDataHandler.Handle(new SaveUserSiteDataCommand(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize]
    [HttpGet("GetSavedUserSiteData")]
    public async Task<IActionResult> GetSavedUserSiteData([FromQuery] GetSavedUserSiteDataRequetsDto model)
    {
        var result = await _getSavedUserSiteDataHandler.Handle(new GetSavedUserSiteDataQuery(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [Authorize]
    [HttpGet("DownloadResultSite")]
    public async Task<IActionResult> DownloadResultSite([FromQuery] DownloadResultSiteRequestDto model)
    {
        var result = await _downloadSiteHandler.Handle(new DownloadResultSiteQuery(model));
        return result.IsSuccess ? File(result.Data.Memory.ToArray(), result.Data.ContentType, result.Data.FileName) : BadRequest(result);
    }

    [HttpPost("HostResultSite")]
    public async Task<IActionResult> HostResultSite([FromBody] CreateGithubRepositoryRequestDto model)
    {

        CreateGithubRepository creator = new CreateGithubRepository();
        bool answer = await creator.CreateRepositoryAsync(model);
        return answer ? Ok() : BadRequest();
    }
}
