using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.ValueObjects.SiteData;
using SiteConstructor.Domain.Repositories;

namespace Infrastructure.Repositories;

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

  public void CreatePhotoFiles(SiteData siteData)
  {
    string folderPath = "./site-creator/themes/first/static/images";
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
      else if (base64String.StartsWith("data:application/pdf"))
        return "pdf";
      else
        throw new Exception("Неизвестный формат файла.");
    }

    List<Image> images = siteData.PhotosSrc;
    try
    {
      if (siteData.FaviconSrc.Count > 0)
      {
        Image favicon = siteData.FaviconSrc[0];
        string faviconPath = $"./site-creator/themes/first/static/favicon.ico";
        string faviconBase64String = favicon.ImageFileBase64String.Split(',')[1];
        byte[] faviconFileBytes = Convert.FromBase64String(faviconBase64String);
        File.WriteAllBytes(faviconPath, faviconFileBytes);
        siteData.FaviconSrc[0].ImageFileBase64String = "./favicon.ico";
      }
      if (siteData.LogoSrc.Count > 0)
      {
        Image logo = siteData.LogoSrc[0];
        string logoPath = $"./site-creator/themes/first/static/images/logo.{GetFileExtension(logo.ImageFileBase64String)}";
        string logoBase64String = logo.ImageFileBase64String.Split(',')[1];
        byte[] logoFileBytes = Convert.FromBase64String(logoBase64String);
        File.WriteAllBytes(logoPath, logoFileBytes);
        siteData.LogoSrc[0].ImageFileBase64String = $"./images/logo.{GetFileExtension(logo.ImageFileBase64String)}";
      }
      for (int i = 0; i < images.Count; i++)
      {
        string filePath = $"./site-creator/themes/first/static/images/main/{i + 1}.{GetFileExtension(images[i].ImageFileBase64String)}";
        string base64String = images[i].ImageFileBase64String.Split(',')[1];
        byte[] fileBytes = Convert.FromBase64String(base64String);
        Console.WriteLine("One file");
        File.WriteAllBytes(filePath, fileBytes);
        siteData.PhotosSrc[i].ImageFileBase64String = $"./images/main/{i + 1}.{GetFileExtension(images[i].ImageFileBase64String)}";
      }
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
    }




  }
}
