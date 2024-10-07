using System.IO.Compression;
using Application.UseCases.Queries.DownloadResultSite.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Domain.Models.ValueObjects.SiteData;
using Domain.Repositories;
using SiteConstructor.Domain.Repositories;

namespace Application.UseCases.Queries.DownloadResultSite;

public class CheckHostNameQueryHandler : IQueryHandler<CheckHostNameQuery, CheckHostNameQueryResult>
{
    private readonly ISiteNameRepository _siteNameRepository;

    public CheckHostNameQueryHandler(
        ISiteNameRepository siteNameRepository)
    {
        _siteNameRepository = siteNameRepository;
    }

    public async Task<CheckHostNameQueryResult> Handle(CheckHostNameQuery query)
    {
        Error? validationError = Validate(query);
        if (validationError != null)
        {
            return CheckHostNameQueryResult.Fail(validationError);
        }
        var hasSite = await _siteNameRepository.HasSiteWithName(query.CheckHostNameDto.SiteHostName);
        return CheckHostNameQueryResult.Ok(new CheckHostNameResultDto() 
        { 
            IsAvailable = !hasSite 
        });
    }

    private Error? Validate(CheckHostNameQuery query)
    {
        if (query.CheckHostNameDto == null)
        {
            return new Error("No data provided");
        }

        if (String.IsNullOrEmpty(query.CheckHostNameDto.SiteHostName))
        {
            return new Error("Site host name is empty");
        }
        return null;
    }
}
