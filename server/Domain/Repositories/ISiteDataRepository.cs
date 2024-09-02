using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.ValueObjects.SiteData;

namespace SiteConstructor.Domain.Repositories;

public interface ISiteDataRepository
{
    public SiteData GetSiteData();
    public void SetOrUpdateData(SiteData siteData);
    public void CreatePhotoFiles(SiteData siteData);
}
