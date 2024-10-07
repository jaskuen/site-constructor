using Application.UseCases.Queries.DownloadResultSite.DTOs;
using Application.UseCases.UseCases;

namespace Application.UseCases.Queries.DownloadResultSite;

public class CheckHostNameQuery : IQuery<CheckHostNameQueryResult>
{
    public readonly CheckHostNameRequestDto CheckHostNameDto;

    public CheckHostNameQuery(CheckHostNameRequestDto checkHostNameDto)
    {
        CheckHostNameDto = checkHostNameDto;
    }
}
