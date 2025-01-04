using Microsoft.AspNetCore.Mvc;
using RestaurantPortfolio.Areas.Admin.Entities.Repositories;
using RestaurantPortfolio.Areas.Admin.ViewModels;

namespace RestaurantPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ClientRepository _clientRepository;
        private readonly AboutRepository _aboutRepository;

        public HomeController(ClientRepository clientRepository, AboutRepository aboutRepository)
        {
            _clientRepository = clientRepository;
            _aboutRepository = aboutRepository;
        }

public async Task<ActionResult> Index()
{
    var clients = await _clientRepository.GetAllAsync();
    var abouts = await _aboutRepository.GetAllAsync();

    var viewModel = new HomeViewModel
    {
        Clients = clients,
        Abouts = abouts
    };

    return View(viewModel);
}


    }
}
