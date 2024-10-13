namespace Application.UseCases.Content.Queries.DownloadResultSite.DTOs;

public class ResultSiteDto
{
    public MemoryStream Memory { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
}
