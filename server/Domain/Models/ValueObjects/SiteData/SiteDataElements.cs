using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.ValueObjects.SiteData;

public class BackgroundColors
{
  public string Main { get; set; }
  public string Additional { get; set; }
  public string Translucent { get; set; }
  public string Navigation { get; set; }
}

public class TextColors
{
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

public class Image
{
  public string Id { get; set; }
  public string UserId { get; set; }
  public string Type { get; set; }
  public string ImageFileBase64String { get; set; }
}

