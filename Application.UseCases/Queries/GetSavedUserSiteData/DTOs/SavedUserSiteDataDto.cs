using System.Reflection.PortableExecutable;
using Application.UseCases.Commands.SaveUserSiteData.DTOs;
using Domain.Models.Entities.UserSiteData;
using Domain.Models.ValueObjects.SiteData;
using Octokit;

namespace Application.UseCases.Queries.GetSavedUserSiteData.DTOs;

public class SavedUserSiteDataDto
{
    public UserSiteDataDto SiteData { get; set; }
    public SavedUserSiteDataDto(UserSiteData siteData)
    {
        SiteData = new();
        SiteData.ColorSchemeName = siteData.ColorSchemeName;
        SiteData.BackgroundColors = new BackgroundColorsDto()
        {
            Main = siteData.BackgroundColors.Main,
            Additional = siteData.BackgroundColors.Additional,
            Translucent = siteData.BackgroundColors.Translucent,
            Navigation = siteData.BackgroundColors.Navigation,
        };
        SiteData.TextColors = new TextColorsDto()
        {
            Main = siteData.TextColors.Main,
            Additional = siteData.TextColors.Additional,
            Translucent = siteData.TextColors.Translucent,
            Accent = siteData.TextColors.Accent,
        };
        SiteData.HeadersFont = siteData.HeadersFont;
        SiteData.LogoBackgroundColor = siteData.LogoBackgroundColor;
        SiteData.RemoveLogoBackground = siteData.RemoveLogoBackground;
        SiteData.Header = siteData.Header;
        SiteData.Description = siteData.Description;
        SiteData.VkLink = siteData.VkLink;
        SiteData.TelegramLink = siteData.TelegramLink;
        SiteData.YoutubeLink = siteData.YoutubeLink;
        SiteData.Images = siteData.Images;
    }
}

public class UserSiteDataDto
{
    // Design
    public string ColorSchemeName { get; set; }
    public BackgroundColorsDto BackgroundColors { get; set; }
    public TextColorsDto TextColors { get; set; }
    public string HeadersFont { get; set; }
    public string MainTextFont { get; set; }
    public string LogoBackgroundColor { get; set; }
    public bool RemoveLogoBackground { get; set; }

    // Content
    public string Header { get; set; }
    public string Description { get; set; }
    public string VkLink { get; set; }
    public string TelegramLink { get; set; }
    public string YoutubeLink { get; set; }
    public ICollection<Image> Images { get; set; }
}


