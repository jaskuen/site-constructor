using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.DownloadResultSite.DTOs;

public class ResultSiteDto
{
    public MemoryStream Memory { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
}
