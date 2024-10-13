using Application.UseCases.Content.Commands.HostResultSite.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;

namespace Application.UseCases.Content.Commands.HostResultSite;

public class HostResultSiteCommand : ICommand<Result>
{
    public readonly HostResultSiteRequestDto RequestDto;
    public HostResultSiteCommand(HostResultSiteRequestDto requestDto)
    {
        RequestDto = requestDto;
    }
}
