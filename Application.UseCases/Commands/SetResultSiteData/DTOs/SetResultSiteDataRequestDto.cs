using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.ValueObjects.SiteData;

namespace Application.UseCases.Commands.GetSiteData.DTOs;

public class SetResultSiteDataRequestDto
{
    public SiteData SiteData { get; set; }
}
