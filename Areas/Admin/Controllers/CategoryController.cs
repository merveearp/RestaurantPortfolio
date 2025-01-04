using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantPortfolio.Areas.Admin.Entities.Repositories;
using RestaurantPortfolio.Areas.Admin.ViewModels;

namespace RestaurantPortfolio.Areas.Admin.Controllers
{


    [Area("Admin")]

    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly INotyfService _notyfService;

        public CategoryController(CategoryRepository categoryRepository, INotyfService notyfService)
        {
            _categoryRepository = categoryRepository;
            _notyfService = notyfService;
        }
//true aktif // false aktif olmayan kayıtlar
       public async Task<ActionResult> Index(bool id)
        {
            bool isActive=id;
            ViewBag.IsActive=isActive;
            var categories =await _categoryRepository.GetAllAsync(isActive);
            return View(categories);
        }
        private List<SelectListItem> GetIconList()
        {
            List<SelectListItem> iconList=[
            new SelectListItem{Text="fas fa-solid fa-egg"},
            new SelectListItem{Text="fas fa-solid fa-utensils"},
            new SelectListItem{Text="fas fa-solid fa-bacon"},
            new SelectListItem{Text="fas fa-solid fa-pizza-slice"},
            new SelectListItem{Text="fas fa-solid fa-bars"},
            new SelectListItem{Text="fas fa-solid fa-mug-hot"},
            new SelectListItem{Text="fas fa-solid fa-fish"},
            new SelectListItem{Text="fas fa-solid fa-cookie"},
            new SelectListItem{Text="fas fa-solid fa-wine-bottle"},
            new SelectListItem{Text="fas fa-solid fa-bowl-food"}, 
            new SelectListItem{Text="fas fa-solid fa-bottle-water"},
            new SelectListItem{Text="fas fa-solid fa-cheese"},
            new SelectListItem{Text="fas fa-solid fa-wheat-awn"},
           ];
           return iconList;
        }
        public async Task<ActionResult> Create()
        {
           
           CategoryCreateViewModel model=new()
           {
            IconList = GetIconList()
           };
            return View(model);    
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryCreateViewModel categoryCreateViewModel)
        {
            if(ModelState.IsValid)
            {
                await _categoryRepository.CreateAsync(categoryCreateViewModel);
                _notyfService.Success("Yeni Kategori eklendi");
                return RedirectToAction("Index",new {Id=true});
            }
        categoryCreateViewModel.IconList=GetIconList();
        return View(categoryCreateViewModel);
        }
        public async Task<ActionResult> Update(int id)
        {
            var category =await _categoryRepository.GetByIdAsync(id);
            CategoryUpdateViewModel categoryUpdateViewModel =new (){
            Id=category.Id,
            Name=category.Name,
            IconList=GetIconList(),
            UpdatedDate=category.UpdatedDate.Year==1 ? null :category.UpdatedDate,
            };
            return View(categoryUpdateViewModel);
            
        }
        [HttpPost]
        public async Task<ActionResult> Update(CategoryUpdateViewModel categoryUpdateViewModel)
        {
            if(ModelState.IsValid)
            {
                await _categoryRepository.UpdateAsync(categoryUpdateViewModel);
                _notyfService.Success("Seçmiş olduğunuz kategori güncellendi");
                return RedirectToAction("Index",new {Id=true});
                
            }
            categoryUpdateViewModel.IconList=GetIconList();
            return View(categoryUpdateViewModel);
        }
        public async Task<ActionResult> Active(int id)
        {
            bool newStatus=await _categoryRepository.ActiveAsync(id);
            return RedirectToAction("Index",new{isActive=newStatus});
        }
        public async Task<ActionResult> Delete(int id)
        {
            await _categoryRepository.DeleteAsync(id);
            _notyfService.Error("Kategori kalıcı olarak silindi");
            return RedirectToAction("Index",new {Id=true});
        }


}
}
