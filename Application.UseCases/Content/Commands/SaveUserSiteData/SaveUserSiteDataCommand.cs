using Application.UseCases.Content.Commands.SaveUserSiteData.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;

namespace Application.UseCases.Content.Commands.SaveUserSiteData;

public class SaveUserSiteDataCommand : ICommand<Result>
{
    public readonly SaveUserSiteDataRequestDto RequestDto;
    public SaveUserSiteDataCommand(SaveUserSiteDataRequestDto requestDto)
    {
        RequestDto = requestDto;
    }
}
