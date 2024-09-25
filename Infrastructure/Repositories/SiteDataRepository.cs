using System.Reflection.Metadata;
using System.Text.Json;
using Domain.Models.ValueObjects.SiteData;
using SiteConstructor.Domain.Repositories;

namespace Infrastructure.Repositories;

public class SiteDataRepository : ISiteDataRepository
{
    private SiteData _siteData = new();
    public SiteData GetSiteData()
    {
        return _siteData;
    }
    public void SetOrUpdateData(SiteData siteData)
    {
        _siteData = siteData;
    }

    public void CreateHugoDirectory()
    {
        string hugoSamplePath = "./site-creator/sample";
        string hugoUserPath = $"./site-creator/{_siteData.UserId}";
        if (!Directory.Exists(hugoSamplePath))
        {
            Directory.CreateDirectory(hugoSamplePath);
        }
        if (!Directory.Exists(hugoUserPath))
        {
            Directory.CreateDirectory(hugoUserPath);
        }
        CopyDirectory(hugoSamplePath, hugoUserPath);
    }

    public void ApplyDataToHugo()
    {
        string folderPath = $"./site-creator/{_siteData.UserId}/themes/first/static/images";
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                File.Delete(file);
            }
            if (Directory.Exists(folderPath + "/main"))
            {
                string[] mainImages = Directory.GetFiles(folderPath + "/main");
                foreach (string image in mainImages)
                {
                    File.Delete(image);
                }
            }
        }
        else
        {
            Console.WriteLine("Папка не существует.");
        }

        static string GetFileExtension(string base64String)
        {
            if (base64String.StartsWith("data:image/jpeg"))
                return "jpg";
            else if (base64String.StartsWith("data:image/png"))
                return "png";
            else if (base64String.StartsWith("data:image/gif"))
                return "gif";
            else
                throw new Exception("Неизвестный формат файла.");
        }

        List<Image> images = _siteData.ContentPageData.PhotosSrc;
        try
        {
            if (_siteData.DesignPageData.FaviconSrc.Count > 0)
            {
                Image favicon = _siteData.DesignPageData.FaviconSrc[0];
                string faviconPath = $"./site-creator/{_siteData.UserId}/themes/first/static/favicon.ico";
                string faviconBase64String = favicon.ImageFileBase64String.Split(',')[1];
                byte[] faviconFileBytes = Convert.FromBase64String(faviconBase64String);
                File.WriteAllBytes(faviconPath, faviconFileBytes);
                _siteData.DesignPageData.FaviconSrc[0].ImageFileBase64String = "./favicon.ico";
            }
            if (_siteData.DesignPageData.LogoSrc.Count > 0)
            {
                Image logo = _siteData.DesignPageData.LogoSrc[0];
                string logoPath = $"./site-creator/{_siteData.UserId}/themes/first/static/images/logo.{GetFileExtension(logo.ImageFileBase64String)}";
                string logoBase64String = logo.ImageFileBase64String.Split(',')[1];
                byte[] logoFileBytes = Convert.FromBase64String(logoBase64String);
                File.WriteAllBytes(logoPath, logoFileBytes);
                _siteData.DesignPageData.LogoSrc[0].ImageFileBase64String = $"./images/logo.{GetFileExtension(logo.ImageFileBase64String)}";
            }
            for (int i = 0; i < images.Count; i++)
            {
                string filePath = $"./site-creator/{_siteData.UserId}/themes/first/static/images/main/{i + 1}.{GetFileExtension(images[i].ImageFileBase64String)}";
                string base64String = images[i].ImageFileBase64String.Split(',')[1];
                byte[] fileBytes = Convert.FromBase64String(base64String);
                File.WriteAllBytes(filePath, fileBytes);
                _siteData.ContentPageData.PhotosSrc[i].ImageFileBase64String = $"./images/main/{i + 1}.{GetFileExtension(images[i].ImageFileBase64String)}";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        string jsonPath = $"./site-creator/{_siteData.UserId}/static/data/data.json";
        File.WriteAllText(jsonPath, JsonSerializer.Serialize(_siteData));
    }

    private void CopyDirectory(string sourceDir, string destDir)
    {
        DirectoryInfo dir = new DirectoryInfo(sourceDir);
        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException($"Source directory does not exist: {sourceDir}");
        }
        Directory.CreateDirectory(destDir);
        foreach (FileInfo file in dir.GetFiles())
        {
            string targetFilePath = Path.Combine(destDir, file.Name);
            file.CopyTo(targetFilePath, true);
        }
        foreach (DirectoryInfo subdir in dir.GetDirectories())
        {
            string newDestinationDir = Path.Combine(destDir, subdir.Name);
            CopyDirectory(subdir.FullName, newDestinationDir);
        }
    }
}
