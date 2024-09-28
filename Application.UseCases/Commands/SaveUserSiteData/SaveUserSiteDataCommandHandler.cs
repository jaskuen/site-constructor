using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Domain.Repositories;
using SiteConstructor.Domain.Repositories;

namespace Application.UseCases.Commands.SaveUserSiteData;

public class SaveUserSiteDataCommandHandler : ICommandHandler<SaveUserSiteDataCommand, Result>
{
    private readonly IUserSiteDataRepository _userSiteDataRepository;
    private readonly IUnitOfWork _unitOfWork;
    public SaveUserSiteDataCommandHandler(
        IUserSiteDataRepository userSiteDataRepository,
        IUnitOfWork unitOfWork)
    {
        _userSiteDataRepository = userSiteDataRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SaveUserSiteDataCommand command)
    {
        Error? validateError = Validate(command);
        if (validateError != null)
        {
            return Result.Fail(validateError);
        }
        var userSiteData = _userSiteDataRepository.Table.FirstOrDefault(d => d.UserId == command.RequestDto.UserSiteData.UserId);

        if (userSiteData == null)
        {
            _userSiteDataRepository.Add(command.RequestDto.UserSiteData);
        }
        else
        {
            userSiteData.ColorSchemeName = command.RequestDto.UserSiteData.ColorSchemeName;
            userSiteData.BackgroundColors = command.RequestDto.UserSiteData.BackgroundColors;
            userSiteData.TextColors = command.RequestDto.UserSiteData.TextColors;
            userSiteData.HeadersFont = command.RequestDto.UserSiteData.HeadersFont;
            userSiteData.MainTextFont = command.RequestDto.UserSiteData.MainTextFont;
            userSiteData.LogoBackgroundColor = command.RequestDto.UserSiteData.LogoBackgroundColor;
            userSiteData.RemoveLogoBackground = command.RequestDto.UserSiteData.RemoveLogoBackground;
            userSiteData.Header = command.RequestDto.UserSiteData.Header;
            userSiteData.Description = command.RequestDto.UserSiteData.Description;
            userSiteData.VkLink = command.RequestDto.UserSiteData.VkLink;
            userSiteData.TelegramLink = command.RequestDto.UserSiteData.TelegramLink;
            userSiteData.YoutubeLink = command.RequestDto.UserSiteData.YoutubeLink;
            userSiteData.Images = command.RequestDto.UserSiteData.Images;
        }

        _unitOfWork.Commit();

        return Result.Ok();
    }

    private Error? Validate(SaveUserSiteDataCommand command)
    {
        if (command.RequestDto == null)
        {
            return new Error("No data provided");
        }
        if (String.IsNullOrWhiteSpace(command.RequestDto.UserSiteData.UserId.ToString()))
        {
            return new Error("No user id");
        }
        return null;
    }
}
