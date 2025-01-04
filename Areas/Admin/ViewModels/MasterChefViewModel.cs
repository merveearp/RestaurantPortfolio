using System;

namespace RestaurantPortfolio.Areas.Admin.ViewModels;

public class MasterChefViewModel
{
    public int Id {get;set;}
	public string? Name {get;set;}
	public string? ChefText {get;set;}
	public string? MasterChefsUrl {get;set;}
	public bool IsActive {get;set;}
	public DateTime? UpdatedDate {get;set;}

}
