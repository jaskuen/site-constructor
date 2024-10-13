using Domain.Models.ValueObjects.SiteData;

namespace Domain.Models.Entities.UserSiteData;

public class UserSiteData : Entity
{
    public int UserId { get; set; }

    // Design
    public string ColorSchemeName { get; set; }
    public BackgroundColors BackgroundColors { get; set; }
    public TextColors TextColors { get; set; }
    public string HeadersFont { get; set; }
    public string MainTextFont { get; set; }
    public string LogoBackgroundColor { get; set; }
    public bool RemoveLogoBackground { get; set; }

    // Content
    //public List<SelectLanguage> Languages { get; set; }
    //public Language MainLanguage { get; set; }
    public string Header { get; set; }
    public string Description { get; set; }
    public string VkLink { get; set; }
    public string TelegramLink { get; set; }
    public string YoutubeLink { get; set; }
    public ICollection<Image> Images { get; set; }
}
