using System;

namespace RestaurantPortfolio.Areas.Admin.ViewModels;

public class MealViewModel
{
    public int Id {get;set;}
	public string? Name {get;set;}
	public string? Ingredient {get;set;}
	public string? MealImageUrl {get;set;}
	public string? CategoryName {get;set;}
	public int Price {get;set;}
	public bool IsActive {get;set;}
	public DateTime? UpdatedDate {get;set;}

}
