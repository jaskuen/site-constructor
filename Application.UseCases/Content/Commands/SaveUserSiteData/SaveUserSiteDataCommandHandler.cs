using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Domain.Models.Entities.UserSiteData;
using Domain.Models.ValueObjects.SiteData;
using Domain.Repositories;

namespace Application.UseCases.Content.Commands.SaveUserSiteData;

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
        var userSiteData = _userSiteDataRepository.Table.FirstOrDefault(d => d.UserId == command.RequestDto.UserId);

        if (userSiteData != null)
        {
            _userSiteDataRepository.Remove(userSiteData);
        }
        UserSiteData newData = new UserSiteData()
        {
            UserId = command.RequestDto.UserId,
            ColorSchemeName = command.RequestDto.ColorSchemeName,
            BackgroundColors = new BackgroundColors()
            {
                Main = command.RequestDto.BackgroundColors.Main,
                Additional = command.RequestDto.BackgroundColors.Additional,
                Translucent = command.RequestDto.BackgroundColors.Translucent,
                Navigation = command.RequestDto.BackgroundColors.Navigation,
            },
            TextColors = new TextColors()
            {
                Main = command.RequestDto.TextColors.Main,
                Additional = command.RequestDto.TextColors.Additional,
                Translucent = command.RequestDto.TextColors.Translucent,
                Accent = command.RequestDto.TextColors.Accent,
            },
            HeadersFont = command.RequestDto.HeadersFont,
            LogoBackgroundColor = command.RequestDto.LogoBackgroundColor,
            RemoveLogoBackground = command.RequestDto.RemoveLogoBackground,
            Header = command.RequestDto.Header,
            Description = command.RequestDto.Description,
            VkLink = command.RequestDto.VkLink,
            TelegramLink = command.RequestDto.TelegramLink,
            YoutubeLink = command.RequestDto.YoutubeLink,
            Images = command.RequestDto.Images,
        };
        _userSiteDataRepository.Add(newData);

        _unitOfWork.Commit();

        return Result.Ok();
    }

    private Error? Validate(SaveUserSiteDataCommand command)
    {
        if (command.RequestDto == null)
        {
            return new Error("No data provided");
        }
        if (string.IsNullOrWhiteSpace(command.RequestDto.UserId.ToString()))
        {
            return new Error("No user id");
        }
        return null;
    }
}
