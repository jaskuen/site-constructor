using Application.UseCases.Content.Queries.GetSavedUserSiteData.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Domain.Models.Entities.UserSiteData;
using Domain.Repositories;

namespace Application.UseCases.Content.Queries.GetSavedUserSiteData;

public class GetSavedUserSiteDataQueryHandler : IQueryHandler<GetSavedUserSiteDataQuery, GetSavedUserSiteDataQueryResult>
{
    private readonly IUserSiteDataRepository _userSiteDataRepository;
    public GetSavedUserSiteDataQueryHandler(IUserSiteDataRepository userSiteDataRepository)
    {
        _userSiteDataRepository = userSiteDataRepository;
    }

    public async Task<GetSavedUserSiteDataQueryResult> Handle(GetSavedUserSiteDataQuery query)
    {
        Error? validationError = Validate(query);
        if (validationError != null)
        {
            return GetSavedUserSiteDataQueryResult.Fail(validationError);
        }
        UserSiteData? userSiteData = await _userSiteDataRepository.GetSiteData(query.GetSavedUserSiteDataDto.UserId);
        if (userSiteData == null)
        {
            return GetSavedUserSiteDataQueryResult.Fail(new Error("No saved data"));
        }
        return GetSavedUserSiteDataQueryResult.Ok(new SavedUserSiteDataDto(userSiteData));
    }

    private Error? Validate(GetSavedUserSiteDataQuery query)
    {
        if (query == null)
        {
            return new Error("No data provided");
        }
        if (string.IsNullOrEmpty(query.GetSavedUserSiteDataDto.UserId.ToString()))
        {
            return new Error("No user id");
        }
        return null;
    }
}
