using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Entities;
using Domain.Models.Entities.UserSiteData;

namespace Domain.Models.ValueObjects.SiteData;

public class BackgroundColors
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Main { get; set; }
    public string Additional { get; set; }
    public string Translucent { get; set; }
    public string Navigation { get; set; }
}

public class TextColors
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Main { get; set; }
    public string Additional { get; set; }
    public string Translucent { get; set; }
    public string Accent { get; set; }
}

public class Language
{
    public string Code { get; set; }
    public string Name { get; set; }
}

public class SelectLanguage : Language
{
    public bool Selected { get; set; }
}

public class Image : Entity
{
    public int UserSiteDataId { get; set; }
    public string Type { get; set; }
    public string ImageFileBase64String { get; set; }
    public UserSiteData UserSiteData { get; set; }
}

public class DesignPageData
{
    public string ColorSchemeName { get; set; }
    public BackgroundColors BackgroundColors { get; set; }
    public TextColors TextColors { get; set; }
    public string HeadersFont { get; set; }
    public string MainTextFont { get; set; }
    public List<Image> LogoSrc { get; set; }
    public string LogoBackgroundColor { get; set; }
    public bool RemoveLogoBackground { get; set; }
    public List<Image> FaviconSrc { get; set; }
}

public class ContentPageData
{
    public List<SelectLanguage> Languages { get; set; }
    public Language MainLanguage { get; set; }
    public string Header { get; set; }
    public string Description { get; set; }
    public string VkLink { get; set; }
    public string TelegramLink { get; set; }
    public string YoutubeLink { get; set; }
    public List<Image> PhotosSrc { get; set; }
}

