using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UseCases.Queries.DownloadResultSite.DTOs;
using Application.UseCases.UseCases;

namespace Application.UseCases.Queries.DownloadResultSite;

public class DownloadResultSiteQuery : IQuery<DownloadResultSiteQueryResult>
{
    public readonly DownloadResultSiteRequestDto DownloadResultSiteDto;

    public DownloadResultSiteQuery( DownloadResultSiteRequestDto downloadResultSiteDto)
    {
        DownloadResultSiteDto = downloadResultSiteDto;
    }
}
