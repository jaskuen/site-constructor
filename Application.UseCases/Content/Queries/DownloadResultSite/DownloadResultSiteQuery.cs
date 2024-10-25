using Application.UseCases.Content.Queries.DownloadResultSite.DTOs;
using Application.UseCases.UseCases;

namespace Application.UseCases.Content.Queries.DownloadResultSite;

public class DownloadResultSiteQuery : IQuery<DownloadResultSiteQueryResult>
{
    public readonly DownloadResultSiteRequestDto DownloadResultSiteDto;

    public DownloadResultSiteQuery(DownloadResultSiteRequestDto downloadResultSiteDto)
    {
        DownloadResultSiteDto = downloadResultSiteDto;
    }
}
