using Application.UseCases.Queries.GetSavedUserSiteData.DTOs;
using Application.UseCases.UseCases;

namespace Application.UseCases.Queries.UploadSavedUserSiteData;

public class GetSavedUserSiteDataQuery : IQuery<GetSavedUserSiteDataQueryResult>
{
    public readonly GetSavedUserSiteDataRequetsDto GetSavedUserSiteDataDto;
    public GetSavedUserSiteDataQuery(GetSavedUserSiteDataRequetsDto getSavedUserSiteDataDto)
    {
        GetSavedUserSiteDataDto = getSavedUserSiteDataDto;
    }
}
