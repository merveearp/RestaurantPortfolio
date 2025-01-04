using System;
using RestaurantPortfolio.Models;

namespace RestaurantPortfolio.Helpers;

public class ImageHelper
{
    private readonly string _imagesFolder;
    private readonly string _folderName;
    //http://localhost:5500/ui/img
    //http://www.enginniyazi.com/ui/img
    public ImageHelper(IWebHostEnvironment environment, IConfiguration configuration)
    {
        _imagesFolder = Path.Combine(environment.WebRootPath, "ui/img");
        _folderName = configuration["BaseUrl"];
    }

    //dotnet514.png
    //folderName=projects
    public async Task<ImageModel> UploadImage(IFormFile image, string folderName)
    {
        if (image == null)
        {
            return new ImageModel
            {
                IsSuccess = false,
                ErrorMessage = "Resim seçilmedi!"
            };
        }
        if (image.Length == 0)
        {
            return new ImageModel
            {
                IsSuccess = false,
                ErrorMessage = "Bozuk resim dosyası!"
            };
        }
        string[] correctExtensions = [".png", ".jpg", ".jpeg"];
        string fileExtension = Path.GetExtension(image.FileName);//.png ya da .jpg ya da .docx
        if (!correctExtensions.Contains(fileExtension))
        {
            return new ImageModel
            {
                IsSuccess = false,
                ErrorMessage = "Geçersiz resim formatı!"
            };
        }
        string targetFolder = Path.Combine(_imagesFolder, folderName);
        //http://localhost:5500/ui/img/projects klasörü var mı yok mu kontrolü
        if (!Directory.Exists(targetFolder))
        {
            Directory.CreateDirectory(targetFolder);
        }

        //825a83fe-8443-42f7-8180-3cafaf56ba7b.jpg
        string imageFileName = $"{Guid.NewGuid()}{fileExtension}";

        //http://localhost:5500/ui/img/projects/825a83fe-8443-42f7-8180-3cafaf56ba7b.jpg  
        string imageUrl = Path.Combine(targetFolder, imageFileName);

        await using (var stream = new FileStream(imageUrl, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }
        string returnImageUrl = Path.Combine(_folderName, "ui/img", folderName, imageFileName);
        return new ImageModel
        {
            ImageUrl = returnImageUrl,
            IsSuccess = true,
        };
    }
}

