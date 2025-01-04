using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantPortfolio.Areas.Admin.Entities.Repositories;
using RestaurantPortfolio.Areas.Admin.ViewModels;

namespace RestaurantPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly SocialRepository _socialRepository;

        public SocialController(INotyfService notyfService, SocialRepository socialRepository)
        {
            _notyfService = notyfService;
            _socialRepository = socialRepository;
        }

        public async Task<ActionResult> Index(bool id)
        {
            bool isActive=id;
            ViewBag.IsActive=isActive;
            var socials =await _socialRepository.GetAllAsync(isActive);
            return View(socials);
        }
        private List<SelectListItem> GetIconList()

        {
            List<SelectListItem> iconList=[
            new SelectListItem{Text="fab fa-github"},
            new SelectListItem{Text="fab fa-twitter-x"},
            new SelectListItem{Text="fab fa-linkedin-in"},
            new SelectListItem{Text="fab fa-medium"},
            new SelectListItem{Text="fab fa-instagram"},
            new SelectListItem{Text="fab fa-pinterest"},
            new SelectListItem{Text="fab fa-youtube"},
            new SelectListItem{Text="fab fa-facebook-f"}

           ];
           return iconList;

        }
        public async Task<ActionResult> Create()
        {
           
           SocialCreateViewModel model=new()
           {
            IconList = GetIconList()
           };
            return View(model);    
        }

        [HttpPost]
        public async Task<ActionResult> Create(SocialCreateViewModel socialCreateViewModel)
        {
            if(ModelState.IsValid)
            {
                await _socialRepository.CreateAsync(socialCreateViewModel);
                _notyfService.Success("Yeni Medya eklendi");
                return RedirectToAction("Index",new {Id=true});
            }
        socialCreateViewModel.IconList=GetIconList();
        return View(socialCreateViewModel);
        }
        public async Task<ActionResult> Update(int id)
        {
            var social =await _socialRepository.GetByIdAsync(id);
            SocialUpdateViewModel socialUpdateViewModel =new (){
            Id=social.Id,
            Name=social.Name,
            Url=social.Url,
            IconList=GetIconList(),
            UpdatedDate=social.UpdatedDate.Year==1 ? null :social.UpdatedDate,
            };
            return View(socialUpdateViewModel);
            
        }
        [HttpPost]
        public async Task<ActionResult> Update(SocialUpdateViewModel socialUpdateViewModel)
        {
            if(ModelState.IsValid)
            {
                await _socialRepository.UpdateAsync(socialUpdateViewModel);
                _notyfService.Success("Seçmiş olduğunuz medya güncellendi");
                return RedirectToAction("Index",new {Id=true});
                
            }
            socialUpdateViewModel.IconList=GetIconList();
            return View(socialUpdateViewModel);
        }
         public async Task<ActionResult> Active(int id)
        {
            bool isActive=await _socialRepository.ActiveAsync(id);
            return RedirectToAction("Index",new{id=isActive});
        }
        public async Task<ActionResult> Delete(int id)
        {
            await _socialRepository.DeleteAsync(id);
            _notyfService.Error("Medyayı kalıcı olarak silindi");
            return RedirectToAction("Index",new {Id=true});
        }



    }
}
