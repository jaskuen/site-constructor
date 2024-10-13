using Domain.Models.Entities;

namespace Domain.Models.ValueObjects.SiteData;

public class SiteData : Entity
{
    public int UserId { get; set; }
    public DesignPageData DesignPageData { get; set; }
    public ContentPageData ContentPageData { get; set; }
}
