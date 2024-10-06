using Application.UseCases.Commands.SaveUserSiteData;
using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Domain.Models.Entities.SiteName;
using Domain.Models.ValueObjects.GitHub;
using Domain.Models.ValueObjects.GitHub.DTOs;
using Domain.Models.ValueObjects.SiteData;
using Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using SiteConstructor.Domain.Repositories;

namespace Application.UseCases.Commands.HostResultSite;

public class HostResultSiteCommandHandler : ICommandHandler<HostResultSiteCommand, Result>
{
    private readonly ISiteNameRepository _siteNameRepository;
    private readonly ISiteDataRepository _siteDataRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly GithubApi _githubApi;
    public HostResultSiteCommandHandler(
        ISiteNameRepository siteNameRepository,
        ISiteDataRepository siteDataRepository,
        IUnitOfWork unitOfWork)
    {
        _siteNameRepository = siteNameRepository;
        _siteDataRepository = siteDataRepository;
        _unitOfWork = unitOfWork;
        _githubApi = new GithubApi();
    }

    public async Task<Result> Handle(HostResultSiteCommand command)
    {
        Error? validateError = Validate(command);
        if (validateError != null)
        {
            return Result.Fail(validateError);
        }

        if (await _siteNameRepository.HasSiteWithName(command.RequestDto.Name))
        {
            return Result.Fail(new Error("Site with that name already exists"));
        }

        _siteNameRepository.Add(new SiteName() 
        { 
            Name = command.RequestDto.Name,
            UserId = command.RequestDto.UserId,
        });
        string hostingSiteFolderPath = 
            $"C:/Users/Jaskuen/Documents/GitHub/StaticData/site-constructor/HostingFolders/{command.RequestDto.UserId}/{command.RequestDto.Name}";
        if (!Directory.Exists(hostingSiteFolderPath))
        {
            Directory.CreateDirectory(hostingSiteFolderPath);
        }
        SiteDataService.BuildHugoSite(command.RequestDto.UserId.ToString());
        _siteDataRepository.CopyDirectory(
            $"./site-creator/{command.RequestDto.UserId}/public",
            hostingSiteFolderPath
        );
        Directory.Delete($"./site-creator/{command.RequestDto.UserId}/public", true);

        await _githubApi.CreateAndCloneRepositoryAsync(new CreateGithubRepositoryRequestDto
        {
            Name = command.RequestDto.Name,
            Description = "",
            Private = false,
            DirectoryPath = hostingSiteFolderPath,
        });
        string siteUrl = await _githubApi.DeployToGitHubPages(command.RequestDto.Name);
        if (siteUrl.IsNullOrEmpty())
        {
            return Result.Fail(new Error("Error while adding to Github Pages"));
        }

        _unitOfWork.Commit();

        return Result.Ok();
    }

    private Error? Validate(HostResultSiteCommand command)
    {
        if (command.RequestDto == null)
        {
            return new Error("No data provided");
        }
        if (string.IsNullOrWhiteSpace(command.RequestDto.UserId.ToString()))
        {
            return new Error("No user id");
        }
        return null;
    }
}
