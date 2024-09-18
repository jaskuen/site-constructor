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

    public void ApplyDataToHugo()
    {
        string staticDataPath = "C:/Users/Jaskuen/Documents/GitHub/StaticData/site-constructor";
        string folderPath = $"{staticDataPath}/{_siteData.UserId}";
        string imagesFolderPath = folderPath + "/images";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);    
        }
        if (!Directory.Exists(imagesFolderPath))
        {
            Directory.CreateDirectory(imagesFolderPath);
        }
        if (!Directory.Exists(imagesFolderPath + "/main"))
        {
            Directory.CreateDirectory(imagesFolderPath + "/main");
        }
        //string[] files = Directory.GetFiles(folderPath);

        //foreach (string file in files)
        //{
        //    File.Delete(file);
        //}
        //if (Directory.Exists(imagesFolderPath + "/main"))
        //{
        //    string[] mainImages = Directory.GetFiles(imagesFolderPath + "/main");
        //    foreach (string image in mainImages)
        //    {
        //        File.Delete(image);
        //    }
        //}

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

        List<Image> images = _siteData.PhotosSrc;
        try
        {
            if (_siteData.FaviconSrc.Count > 0)
            {
                Image favicon = _siteData.FaviconSrc[0];
                string faviconPath = imagesFolderPath + "/favicon.ico";
                string faviconBase64String = favicon.ImageFileBase64String.Split(',')[1];
                byte[] faviconFileBytes = Convert.FromBase64String(faviconBase64String);
                File.WriteAllBytes(faviconPath, faviconFileBytes);
                _siteData.FaviconSrc[0].ImageFileBase64String = faviconPath;
            }
            else
            {
                _siteData.FaviconSrc[0].ImageFileBase64String = staticDataPath + "/favicon_default.ico";
            }
            if (_siteData.LogoSrc.Count > 0)
            {
                Image logo = _siteData.LogoSrc[0];
                string logoPath = imagesFolderPath + $"/logo.{GetFileExtension(logo.ImageFileBase64String)}";
                string logoBase64String = logo.ImageFileBase64String.Split(',')[1];
                byte[] logoFileBytes = Convert.FromBase64String(logoBase64String);
                File.WriteAllBytes(logoPath, logoFileBytes);
                _siteData.LogoSrc[0].ImageFileBase64String = logoPath;
            }
            for (int i = 0; i < images.Count; i++)
            {
                string filePath = imagesFolderPath + $"/main/{images[i].Id}.{GetFileExtension(images[i].ImageFileBase64String)}";
                string base64String = images[i].ImageFileBase64String.Split(',')[1];
                byte[] fileBytes = Convert.FromBase64String(base64String);
                File.WriteAllBytes(filePath, fileBytes);
                _siteData.PhotosSrc[i].ImageFileBase64String = filePath;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        string jsonPath = folderPath + "/data.json";
        System.IO.File.WriteAllText(jsonPath, JsonSerializer.Serialize(_siteData));
    }
}
