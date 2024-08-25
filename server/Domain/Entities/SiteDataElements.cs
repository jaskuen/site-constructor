using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class BackgroundColors
{
    public string Main { get; set; }
    public string Additional { get; set; }
    public string Transculent { get; set; }
    public string Navigation { get; set; }
}

public class TextColors
{
    public string Main { get; set; }
    public string Additional { get; set; }
    public string Transculent { get; set; }
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

