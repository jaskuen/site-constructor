using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories;

public interface ISiteDataRepository
{
    public SiteData GetSiteData();
    public void SetOrUpdateData(SiteData siteData);
}
