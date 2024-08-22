using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteConstructor.Domain.Entities;

namespace SiteConstructor.Domain.Repositories;

public interface ISiteDataRepository
{
    public SiteData GetSiteData();
    public void SetOrUpdateData(SiteData siteData);
    public void CreatePhotoFiles(SiteData siteData);
}
