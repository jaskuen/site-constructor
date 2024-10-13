using Application.UseCases.Content.Queries.CheckHostName.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Domain.Repositories;

namespace Application.UseCases.Content.Queries.CheckHostName;

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

        if (string.IsNullOrEmpty(query.CheckHostNameDto.SiteHostName))
        {
            return new Error("Site host name is empty");
        }
        return null;
    }
}
