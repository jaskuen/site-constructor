using Domain.Models.ValueObjects.SiteData;

namespace Application.UseCases.Content.Commands.SaveUserSiteData.DTOs;

public class SaveUserSiteDataRequestDto
{
    public int UserId { get; set; }

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

public class TextColorsDto
{
    public string Main { get; set; }
    public string Additional { get; set; }
    public string Translucent { get; set; }
    public string Accent { get; set; }
}

public class BackgroundColorsDto
{
    public string Main { get; set; }
    public string Additional { get; set; }
    public string Translucent { get; set; }
    public string Navigation { get; set; }
}
