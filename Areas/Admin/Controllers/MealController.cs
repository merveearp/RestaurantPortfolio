using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantPortfolio.Areas.Admin.Entities.Repositories;
using RestaurantPortfolio.Areas.Admin.ViewModels;
using RestaurantPortfolio.Helpers;

namespace RestaurantPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MealController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly ImageHelper _imageHelper;
        private readonly CategoryRepository _categoryRepository;
        private readonly MealRepository _mealRepository;

        public MealController(INotyfService notyfService, ImageHelper imageHelper, CategoryRepository categoryRepository, MealRepository mealRepository)
        {
            _notyfService = notyfService;
            _imageHelper = imageHelper;
            _categoryRepository = categoryRepository;
            _mealRepository = mealRepository;
        }

        public async Task<ActionResult> Index(bool id)
        {
            bool isActive =id;
            ViewBag.IsActive=isActive;
            var meal=await _mealRepository.GetAllAsync(isActive);
            return View(meal);
        }
        
        [NonAction]
        private async Task<List<SelectListItem>> GenerateCategoryListAsync()
        {
            var categories=await _categoryRepository.GetAllAsync(true);
            List<SelectListItem> categoriesSelectList=[];
            foreach (var category in categories)
            {
                categoriesSelectList.Add(new SelectListItem
                {
                    Text=category.Name,
                    Value=category.Id.ToString()
                });
            }
            return categoriesSelectList;
        }
        public async Task<ActionResult> Create()
        {
            MealCreateViewModel model=new()
            {
                CategoryList=await GenerateCategoryListAsync()

            };
            return View(model);

        }
        [HttpPost]
        public async Task<ActionResult> Create(MealCreateViewModel model,IFormFile image)
        {
            
            var imageModel=await _imageHelper.UploadImage(image,"meals");
            if(!imageModel.IsSuccess)
            {
                ViewBag.ImageError=imageModel.ErrorMessage;
                _notyfService.Error("Hata oluştu ilgili alanları kontrol ediniz!");
                return View(model);

            }
            model.MealImageUrl=imageModel.ImageUrl;


             if(ModelState.IsValid)
            {
                await _mealRepository.CreateAsync(model);
                _notyfService.Success("Yeni Yemek Kaydı eklendi");
                return RedirectToAction("Index",new {Id=true});
            }
        model.CategoryList=await GenerateCategoryListAsync();
        return View(model);

        }
         public async Task<ActionResult> Update(int id)
        {
            var meal =await _mealRepository.GetByIdAsync(id);

            if(meal !=null){
            MealUpdateViewModel mealUpdateViewModel =new (){
            Id=meal.Id,
            Name=meal.Name,
            Ingredient=meal.Ingredient,
            Price=meal.Price,
            CategoryId=meal.CategoryId,
            MealImageUrl=meal.MealImageUrl,
            IsActive=meal.IsActive,
            UpdatedDate=meal.UpdatedDate.Year==1 ? null :meal.UpdatedDate,
            CategoryList=await GenerateCategoryListAsync()
            };
            return View(mealUpdateViewModel);
            }
            return RedirectToAction("Index",new {Id=true});
              
        }
        [HttpPost]
        public async Task<ActionResult> Update(MealUpdateViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (model.Image != null)
                //Eski resim kullanılmayıp, yeni resim seçilmişse
                {

                    var imageModel = await _imageHelper.UploadImage(model.Image, "meals");
                    if (!imageModel.IsSuccess)
                    {
                       
                        ViewBag.ImageError = imageModel.ErrorMessage;
                        _notyfService.Error("Bir hata oluştu, ilgili alanları kontrol ediniz.");
                        return View(model);
                    }
                    model.MealImageUrl = imageModel.ImageUrl;
                }

                await _mealRepository.UpdateAsync(model);
                _notyfService.Success("Seçmiş olduğunuz yemek kaydı güncellendi");
                return RedirectToAction("Index",new {Id=true});
                
            }
            model.CategoryList=await GenerateCategoryListAsync();
            return View(model);
        }
        public async Task<ActionResult> Active(int id)
        {
            bool isActive=await _mealRepository.ActiveAsync(id);
            return RedirectToAction("Index",new{id=isActive});
        }
        public async Task<ActionResult> Delete(int id)
        {
            await _mealRepository.DeleteAsync(id);
            _notyfService.Error("Yemek Kaydı kalıcı olarak silindi");
            return RedirectToAction("Index",new {Id=true});
        }




    }
}
