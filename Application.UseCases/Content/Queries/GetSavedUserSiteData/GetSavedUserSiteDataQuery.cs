using Application.UseCases.Content.Queries.GetSavedUserSiteData.DTOs;
using Application.UseCases.UseCases;

namespace Application.UseCases.Content.Queries.GetSavedUserSiteData;

public class GetSavedUserSiteDataQuery : IQuery<GetSavedUserSiteDataQueryResult>
{
    public readonly GetSavedUserSiteDataRequetsDto GetSavedUserSiteDataDto;
    public GetSavedUserSiteDataQuery(GetSavedUserSiteDataRequetsDto getSavedUserSiteDataDto)
    {
        GetSavedUserSiteDataDto = getSavedUserSiteDataDto;
    }
}
