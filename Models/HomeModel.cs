using System;
using RestaurantPortfolio.Models.Entities;

namespace RestaurantPortfolio.Models;

public class HomeModel
{
    public AppSetting? AppSetting { get; set; }
    public IEnumerable<About>? Abouts { get; set; }
    public IEnumerable<Social>? Socials { get; set; }
    public IEnumerable<Service>? Services { get; set; }
    public IEnumerable<MasterChef>? MasterChefs { get; set; }
    public IEnumerable<Category>? Categories { get; set; }
    public IEnumerable<Meal>? Meals { get; set; }
    public IEnumerable<Client>? Clients { get; set; }
    public Reservation? Reservation { get; set; }
    public string? ActivePage { get; set; }


}
