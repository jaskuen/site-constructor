using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.ValueObjects.SiteData;
using Domain.Models.Entities.LocalUser;

namespace Domain.Models.Entities.UserSiteData;

public class UserSiteData : Entity
{
    //public UserSiteData(SiteData siteData)
    //{
    //    ColorSchemeName = siteData.DesignPageData.ColorSchemeName;
    //    BackgroundColors = siteData.DesignPageData.BackgroundColors;
    //    TextColors = siteData.DesignPageData.TextColors;
    //    HeadersFont = siteData.DesignPageData.HeadersFont;
    //    MainTextFont = siteData.DesignPageData.MainTextFont;
    //    LogoSrc = siteData.DesignPageData.LogoSrc;
    //    LogoBackgroundColor = siteData.DesignPageData.LogoBackgroundColor;
    //    RemoveLogoBackground = siteData.DesignPageData.RemoveLogoBackground;
    //    FaviconSrc = siteData?.DesignPageData?.FaviconSrc;
    //    Languages = siteData.ContentPageData?.Languages;
    //    MainLanguage = siteData?.ContentPageData?.MainLanguage;
    //    Header = siteData?.ContentPageData?.Header;
    //    Description = siteData?.ContentPageData?.Description;
    //    VkLink = siteData?.ContentPageData?.VkLink;
    //    TelegramLink = siteData.ContentPageData?.TelegramLink;
    //    YoutubeLink = siteData.ContentPageData.YoutubeLink;
    //    PhotosSrc = siteData.ContentPageData?.PhotosSrc;
    //}
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
