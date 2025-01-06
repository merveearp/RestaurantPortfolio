using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantPortfolio.Models;
using RestaurantPortfolio.Models.Entities;

namespace RestaurantPortfolio.Controllers;

public class HomeController : Controller
{
    private readonly IDbConnection _dbConnection;

    public HomeController(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IActionResult> Index()
    {
        var appSettings=(await _dbConnection.QueryAsync<AppSetting>("SELECT*FROM AppSettings")).First();

        var abouts =await _dbConnection.QueryAsync<About>("SELECT*FROM Abouts WHERE IsActive=1");

        var services=await _dbConnection.QueryAsync<Service>("SELECT*FROM Services WHERE IsActive=1");

        var socials=await _dbConnection.QueryAsync<Social>("SELECT*FROM Socials WHERE IsActive=1"); 

        var masterchefs=await _dbConnection.QueryAsync<MasterChef>("SELECT * FROM MasterChefs WHERE IsActive=1");
        
        var clients =await _dbConnection.QueryAsync<Client>("SELECT*FROM Clients");

        var categories =await _dbConnection.QueryAsync<Category>("SELECT*FROM Categories");

        // var reservations=(await _dbConnection.QueryAsync<Reservation>("SELECT*FROM Reservations ")).First();
        
        
     HomeModel model=new(){
        AppSetting= appSettings,
        Abouts=abouts,
        Services=services,
        Socials=socials,
        MasterChefs=masterchefs,
        Clients=clients,
        Categories=categories,
        // Reservation=reservations,
        ActivePage ="Ana Sayfa"
    
    };
    return View(model);
    }

    public async Task<IActionResult> About()
    {
        var appSettings=(await _dbConnection.QueryAsync<AppSetting>("SELECT*FROM AppSettings")).First();

        var socials=await _dbConnection.QueryAsync<Social>("SELECT*FROM Socials WHERE IsActive=1"); 

        var abouts =await _dbConnection.QueryAsync<About>("SELECT*FROM Abouts WHERE IsActive=1");

        HomeModel model=new(){
        AppSetting= appSettings,
        Abouts=abouts,
        Socials=socials,
        ActivePage ="Hakkımızda"
    
    };
    return View(model);
    }

    public async Task<IActionResult> Menu()
    {

        var appSettings=(await _dbConnection.QueryAsync<AppSetting>("SELECT*FROM AppSettings")).First();

        var masterchefs=await _dbConnection.QueryAsync<MasterChef>("SELECT * FROM MasterChefs WHERE IsActive=1");

        var categories =await _dbConnection.QueryAsync<Category>("SELECT*FROM Categories WHERE IsActive=1");

        var meals = await _dbConnection.QueryAsync<Meal>("SELECT*FROM Meals WHERE IsActive=1 ");

        var socials=await _dbConnection.QueryAsync<Social>("SELECT*FROM Socials WHERE IsActive=1"); 

         HomeModel model =new()
         {
            AppSetting=appSettings,
            MasterChefs=masterchefs,
            Categories=categories,
            Meals=meals,
            Socials=socials,
            ActivePage="Mutfağımız"

         };
         return View(model);

    }
  
  public async Task<IActionResult> Service()
  {
    
        var appSettings=(await _dbConnection.QueryAsync<AppSetting>("SELECT*FROM AppSettings")).First();

        var socials=await _dbConnection.QueryAsync<Social>("SELECT*FROM Socials WHERE IsActive=1"); 

        var services=await _dbConnection.QueryAsync<Service>("SELECT*FROM Services WHERE IsActive=1");

        HomeModel model =new(){

        AppSetting=appSettings,
        Socials=socials,
        Services=services,
        ActivePage="Hizmetlerimiz"

        };

        return View(model);
    
  }
  
  [HttpGet]
   public async Task<IActionResult> Reservation()
    {
        var appSettings=(await _dbConnection.QueryAsync<AppSetting>("SELECT*FROM AppSettings")).First();
    
        var socials=await _dbConnection.QueryAsync<Social>("SELECT*FROM Socials WHERE IsActive=1"); 
        

        HomeModel model =new()
        {
            AppSetting=appSettings,
            Socials=socials,
            ActivePage="Rezervasyon"

        };

        return View(model);

    }

[HttpPost]
public async Task<IActionResult> Reservation(Reservation reservation)
{
    if (!ModelState.IsValid)
    {
        var appSettings=(await _dbConnection.QueryAsync<AppSetting>("SELECT*FROM AppSettings")).First();
   
        var socials=await _dbConnection.QueryAsync<Social>("SELECT*FROM Socials WHERE IsActive=1"); 


        HomeModel model =new()
        {
            AppSetting=appSettings,
            Socials=socials,
            Reservation=reservation,
            ActivePage="Rezervasyon"

        };

        return View(reservation);
    }
    reservation.SendingTime=DateTime.Now;
    var result =await _dbConnection.ExecuteAsync( "INSERT INTO Reservations (Name, Email, PhoneNumber, PeopleCount, ReservationDate) VALUES (@Name, @Email, @PhoneNumber, @PeopleCount, @ReservationDate)", reservation);
    return RedirectToAction("Index");
}

}
