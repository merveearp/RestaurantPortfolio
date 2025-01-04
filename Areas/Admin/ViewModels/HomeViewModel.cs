using System;
using RestaurantPortfolio.Models.Entities;

namespace RestaurantPortfolio.Areas.Admin.ViewModels;

public class HomeViewModel
{
    public IEnumerable<About>? Abouts { get; set; }
    public IEnumerable<Client>? Clients { get; set; }

}
