using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class SiteData
{
    // Design page data
    public string ColorSchemeName { get; set; }
    public string HeadersFont { get; set; }
    public string MainTextFont { get; set; }
    public List<string> LogoSrc { get; set; }
    public string LogoBackgroundColor { get; set; }
    public bool RemoveLogoBackground { get; set; }
    public List<string> FaviconSrc { get; set; }
    // Content page data
    public List<SelectLanguage> Languages { get; set; }
    public Language MainLanguage { get; set; }
    public string Header {  get; set; }
    public string Description { get; set; }
    public string VkLink { get; set; }
    public string TelegramLink { get; set; }
    public string YoutubeLink { get; set; }
    public List<string> PhotosSrc { get; set; }
}
