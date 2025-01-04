using System.Data;
using AspNetCoreHero.ToastNotification.Abstractions;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantPortfolio.Areas.Admin.Entities.Repositories;
using RestaurantPortfolio.Areas.Admin.ViewModels;
using RestaurantPortfolio.Helpers;

namespace RestaurantPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppSettingController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly IDbConnection _dbConnection;
        private readonly ImageHelper _imageHelper;

        public AppSettingController(INotyfService notyfService, IDbConnection dbConnection, ImageHelper imageHelper)
        {
            _notyfService = notyfService;
            _dbConnection = dbConnection;
            _imageHelper = imageHelper;
        }


        [NonAction]
        private async Task<EditGeneralViewModel> GetGeneralSettings()
        {
            string query = @"
                    SELECT BrandName,HeroTitle,HeroText,HeroImageUrl,AboutSubtitle,AboutText,MenuText FROM AppSettings
                ";
            var model = (await _dbConnection.QueryAsync<EditGeneralViewModel>(query)).First();
            return model;
    }
        [NonAction]
        private async Task<EditContactViewModel> GetContactSettings()
        {
            string query = @"
                    SELECT AddressText,AddressDistrict,AddressProvince,PhoneNumber,Email FROM AppSettings
                ";
            var model = (await _dbConnection.QueryAsync<EditContactViewModel>(query)).First();
            return model;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> EditGeneralSettings()
        {
            var model = await GetGeneralSettings();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditGeneralSettings(EditGeneralViewModel model)
        {
            if (ModelState.IsValid)
            {
                 if (model.HeroText == "<p><br></p>" || String.IsNullOrEmpty(model.HeroText))
                {
                    ViewBag.HeroTextErrorMessage = "Boş bırakılamaz";
                    _notyfService.Warning("Başlık metnini boş bırakmamalısınız.");
                    return View(model);
                }
                 if (model.AboutText == "<p><br></p>" || String.IsNullOrEmpty(model.AboutText))
                {
                    ViewBag.AboutTextErrorMessage = "Boş bırakılamaz";
                    _notyfService.Warning("Hakkında metnini boş bırakmamalısınız.");
                    return View(model);
                }
                if (model.HeroImageUrl != null)
                {
                    var imageModel = await _imageHelper.UploadImage(model.HeroImage, "appsettings");
                    if (!imageModel.IsSuccess)
                    {
                        ViewBag.HeroImageError = imageModel.ErrorMessage;
                        _notyfService.Error(imageModel.ErrorMessage);
                        return View(model);
                    }
                    model.HeroImageUrl = imageModel.ImageUrl;
                }
                string query = @"
                        UPDATE AppSettings
                        SET BrandName=@BrandName,HeroTitle=@HeroTitle,HeroText=@HeroText,HeroImageUrl=@HeroImageUrl,AboutSubtitle=@AboutSubtitle,AboutText=@AboutText,MenuText=@MenuText
                    ";
                await _dbConnection.ExecuteAsync(query, model);
                _notyfService.Success("Genel ayarlar başarıyla kaydedildi!");
                return View(model);
            }
            _notyfService.Error("Bir hata oluştu, ilgili alanları kontrol ediniz.");
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> EditContactSettings()
        {
            var model = await GetContactSettings();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditContactSettings(EditContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                string query = @"
                        UPDATE AppSettings
                        SET AddressText=@AddressText,AddressDistrict=@AddressDistrict,AddressProvince=@AddressProvince,PhoneNumber=@PhoneNumber,Email=@Email
                    ";
                await _dbConnection.ExecuteAsync(query, model);
                _notyfService.Success("İletişim ayarları başarıyla kaydedildi.");
                return View(model);
            }
            _notyfService.Error("Bir hata oluştu, ilgili alanları kontrol ediniz.");
            return View(model);
        }






    }
}
