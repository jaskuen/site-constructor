using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Application.UseCases.Queries.DownloadResultSite.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Domain.Models.ValueObjects.SiteData;
using SiteConstructor.Domain.Repositories;

namespace Application.UseCases.Queries.DownloadResultSite;

public class DownloadResultSiteQueryHandler : IQueryHandler<DownloadResultSiteQuery, DownloadResultSiteQueryResult>
{
    private readonly ISiteDataRepository _siteDataRepository;

    public DownloadResultSiteQueryHandler(
        ISiteDataRepository siteDataRepository)
    {
        _siteDataRepository = siteDataRepository;
    }

    public async Task<DownloadResultSiteQueryResult> Handle( DownloadResultSiteQuery query)
    {
        Error? validationError = Validate(query);
        if (validationError != null)
        {
            return DownloadResultSiteQueryResult.Fail(validationError);
        }
        try
        {
            SiteDataService.BuildHugoSite(query.DownloadResultSiteDto.UserId.ToString());
        }
        catch (Exception ex)
        {
            return DownloadResultSiteQueryResult.Fail(new Error(ex.Message));
        }
        string siteFolderPath = Path.Combine($"./site-creator/{query.DownloadResultSiteDto.UserId}/public");
        string zipFileName = "result.zip";

        string tempZipFilePath = Path.Combine(Path.GetTempPath(), zipFileName);

        if (File.Exists(tempZipFilePath))
        {
            File.Delete(tempZipFilePath);
        }

        ZipFile.CreateFromDirectory(siteFolderPath, tempZipFilePath);

        var memory = new MemoryStream();
        using (var stream = new FileStream(tempZipFilePath, FileMode.Open))
        {
            stream.CopyTo(memory);
        }

        memory.Position = 0;

        File.Delete(tempZipFilePath);
        Directory.Delete(siteFolderPath, true);

        return DownloadResultSiteQueryResult.Ok(new ResultSiteDto
        {
            Memory = memory,
            FileName = query.DownloadResultSiteDto.FileName.Trim(),
            ContentType = "application/zip"
        });
    }

    private Error? Validate(DownloadResultSiteQuery query)
    {
        if (query.DownloadResultSiteDto == null)
        {
            return new Error("No data provided");
        }

        if (String.IsNullOrEmpty(query.DownloadResultSiteDto.UserId.ToString()))
        {
            return new Error("Error, try to reauthenticate");
        }

        if (String.IsNullOrEmpty(query.DownloadResultSiteDto.FileName))
        {
            return new Error("Site archive name is empty");
        }
        return null;
    }
}
