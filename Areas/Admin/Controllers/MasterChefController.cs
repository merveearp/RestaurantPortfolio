using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RestaurantPortfolio.Areas.Admin.Entities.Repositories;
using RestaurantPortfolio.Helpers;
using RestaurantPortfolio.Areas.Admin.ViewModels;
using RestaurantPortfolio.Models.Entities;

namespace RestaurantPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MasterChefController : Controller
    {
        
        private readonly ImageHelper _imageHelper;
        private readonly MasterChefRepository _masterChefRepository;
        private readonly INotyfService _notyfService;

        public MasterChefController(ImageHelper imageHelper, MasterChefRepository masterChefRepository, INotyfService notyfService)
        {
            _imageHelper = imageHelper;
            _masterChefRepository = masterChefRepository;
            _notyfService = notyfService;
        }

        public async Task<ActionResult> Index(bool id)
        {
            bool isActive =id;
            ViewBag.IsActive=isActive;
            var masterchef=await _masterChefRepository.GetAllAsync(isActive);
            return View(masterchef);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(MasterChefCreateViewModel model,IFormFile image)
        {
            
            var imageModel=await _imageHelper.UploadImage(image,"masterchefs");
            if(!imageModel.IsSuccess)
            {
                ViewBag.ImageError=imageModel.ErrorMessage;
                _notyfService.Error("Hata oluştu ilgili alanları kontrol ediniz!");
                return View(model);

            }
            model.MasterChefsUrl=imageModel.ImageUrl;


             if(ModelState.IsValid)
            {
                await _masterChefRepository.CreateAsync(model);
                _notyfService.Success("Yeni Şef Kaydı eklendi");
                return RedirectToAction("Index",new {Id=true});
            }
        return View(model);

        }
        public async Task<ActionResult> Update(int id)
        {
            var masterChef =await _masterChefRepository.GetByIdAsync(id);

            if(masterChef !=null){
             MasterChefUpdateViewModel masterChefUpdateViewModel =new (){
            Id=masterChef.Id,
            Name=masterChef.Name,
            ChefText=masterChef.ChefText,
            MasterChefsUrl=masterChef.MasterChefsUrl,
            UpdatedDate=masterChef.UpdatedDate.Year==1 ? null :masterChef.UpdatedDate,
            };
            return View(masterChefUpdateViewModel);
            }
            return RedirectToAction("Index",new {Id=true});
            
            
        }
        [HttpPost]
        public async Task<ActionResult> Update(MasterChefUpdateViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (model.Image != null)
                //Eski resim kullanılmayıp, yeni resim seçilmişse
                {

                    var imageModel = await _imageHelper.UploadImage(model.Image, "masterchefs");
                    if (!imageModel.IsSuccess)
                    {
                       
                        ViewBag.ImageError = imageModel.ErrorMessage;
                        _notyfService.Error("Bir hata oluştu, ilgili alanları kontrol ediniz.");
                        return View(model);
                    }
                    model.MasterChefsUrl = imageModel.ImageUrl;
                }

                await _masterChefRepository.UpdateAsync(model);
                _notyfService.Success("Seçmiş olduğunuz şef güncellendi");
                return RedirectToAction("Index",new {Id=true});
                
            }
            return View(model);
        }
        public async Task<ActionResult> Active(int id)
        {
            bool isActive=await _masterChefRepository.ActiveAsync(id);
            return RedirectToAction("Index",new{id=isActive});
        }
        public async Task<ActionResult> Delete(int id)
        {
            await _masterChefRepository.DeleteAsync(id);
            _notyfService.Error("Şef kalıcı olarak silindi");
            return RedirectToAction("Index",new {Id=true});
        }


    }
}
