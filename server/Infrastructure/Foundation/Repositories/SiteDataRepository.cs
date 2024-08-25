using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class SiteDataRepository : ISiteDataRepository
{
    public SiteData _siteData = new();
    public SiteData GetSiteData()
    {
        return _siteData;
    }
    public void SetOrUpdateData(SiteData siteData)
    {
        _siteData = siteData;
    }
}
