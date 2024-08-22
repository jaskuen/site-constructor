using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteConstructor.Domain.Entities;
using SiteConstructor.Domain.Repositories;

namespace SiteConstructor.Infrastructure.Foundation.Repositories;

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
        string folderPath = "../../../site-creator/themes/first/static/images/main";
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                File.Delete(file);
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
        Image favicon = siteData.FaviconSrc[0];
        Image logo = siteData.LogoSrc[0];
        for (int i = 0; i < images.Count; i++)
        {
            string filePath = $"../../../site-creator/themes/first/static/images/main/${i}.${GetFileExtension(images[i].ImageFileBase64String)}";
            string base64String = images[i].ImageFileBase64String.Split(',')[1];
            byte[] fileBytes = Convert.FromBase64String(base64String);
            File.WriteAllBytes(filePath, fileBytes);
        }
    }
}
