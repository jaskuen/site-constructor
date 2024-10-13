using Application.UseCases.Content.Queries.CheckHostName.DTOs;
using Application.UseCases.UseCases;

namespace Application.UseCases.Content.Queries.CheckHostName;

public class CheckHostNameQuery : IQuery<CheckHostNameQueryResult>
{
    public readonly CheckHostNameRequestDto CheckHostNameDto;

    public CheckHostNameQuery(CheckHostNameRequestDto checkHostNameDto)
    {
        CheckHostNameDto = checkHostNameDto;
    }
}
