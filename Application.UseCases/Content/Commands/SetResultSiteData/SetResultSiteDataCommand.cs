using Application.UseCases.Content.Commands.SetResultSiteData.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;

namespace Application.UseCases.Content.Commands.SetResultSiteData;

public class SetResultSiteDataCommand : ICommand<Result>
{
    public readonly SetResultSiteDataRequestDto RequestDto;
    public SetResultSiteDataCommand(SetResultSiteDataRequestDto requestDto)
    {
        RequestDto = requestDto;
    }
}
