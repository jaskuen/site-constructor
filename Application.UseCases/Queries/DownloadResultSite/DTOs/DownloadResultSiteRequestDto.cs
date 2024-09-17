using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.DownloadResultSite.DTOs;

public class DownloadResultSiteRequestDto
{
    public int UserId { get; set; }
    public string FileName { get; set; }
}
