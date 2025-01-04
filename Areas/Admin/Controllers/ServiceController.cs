using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantPortfolio.Areas.Admin.Entities.Repositories;
using RestaurantPortfolio.Areas.Admin.ViewModels;

namespace RestaurantPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly ServiceRepository _serviceRepository;
        private readonly INotyfService _notyfService;

        public ServiceController(ServiceRepository serviceRepository, INotyfService notyfService)
        {
            _serviceRepository = serviceRepository;
            _notyfService = notyfService;
        }
       public async Task<ActionResult> Index(bool id)
        {
            bool isActive=id;
            ViewBag.IsActive=isActive;
            var services =await _serviceRepository.GetAllAsync(isActive);
            return View(services);
        }
        private List<SelectListItem> GetIconList()
        {
            List<SelectListItem> iconList=[
            new SelectListItem{Text="fas fa-3x fa-user-tie"},
            new SelectListItem{Text="fas fa-3x fa-utensils"},
            new SelectListItem{Text="fas fa-3x fa-cart-plus"},
            new SelectListItem{Text="fas fa-3x fa-headset"},
            new SelectListItem{Text="fas fa-3x fa-calendar-check"},
            new SelectListItem{Text="fas fa-3 fa-money-check-dollar"},
            new SelectListItem{Text="fas fa-3x fa-kitchen-set"},
            new SelectListItem{Text="fas fa-3x fa-bell-concierge"},
            new SelectListItem{Text="fas fa-3x fa-cart-shopping"}

           ];
           return iconList;
        }
        public async Task<ActionResult> Create()
        {
           
           ServiceCreateViewModel model=new()
           {
            IconList = GetIconList()
           };
            return View(model);    
        }

        [HttpPost]
        public async Task<ActionResult> Create(ServiceCreateViewModel serviceCreateViewModel)
        {
            if(ModelState.IsValid)
            {
                await _serviceRepository.CreateAsync(serviceCreateViewModel);
                _notyfService.Success("Yeni Hizmet eklendi");
                return RedirectToAction("Index",new {Id=true});
            }
        serviceCreateViewModel.IconList=GetIconList();
        return View(serviceCreateViewModel);
        }
        public async Task<ActionResult> Update(int id)
        {
            var service =await _serviceRepository.GetByIdAsync(id);
            ServiceUpdateViewModel serviceUpdateViewModel =new (){
            Id=service.Id,
            Title=service.Title,
            Description=service.Description,
            IconList=GetIconList(),
            UpdatedDate=service.UpdatedDate.Year==1 ? null :service.UpdatedDate,
            };
            return View(serviceUpdateViewModel);
            
        }
        [HttpPost]
        public async Task<ActionResult> Update(ServiceUpdateViewModel serviceUpdateViewModel)
        {
            if(ModelState.IsValid)
            {
                await _serviceRepository.UpdateAsync(serviceUpdateViewModel);
                _notyfService.Success("Seçmiş olduğunuz hizmet güncellendi");
                return RedirectToAction("Index",new {Id=true});
                
            }
            serviceUpdateViewModel.IconList=GetIconList();
            return View(serviceUpdateViewModel);
        }
        public async Task<ActionResult> Active(int id)
        {
            bool isActive=await _serviceRepository.ActiveAsync(id);
            return RedirectToAction("Index",new{id=isActive});
        }
        public async Task<ActionResult> Delete(int id)
        {
            await _serviceRepository.DeleteAsync(id);
            _notyfService.Error("Hizmet kalıcı olarak silindi");
            return RedirectToAction("Index",new {Id=true});
        }

    }
}
