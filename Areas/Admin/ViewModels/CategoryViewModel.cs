using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantPortfolio.Areas.Admin.ViewModels;

public class CategoryViewModel
{
   
    public int Id {get;set;}
	public string? Name {get;set;}
	public string? Icon { get; set; }
    public List<SelectListItem>? IconList { get; set; }
	public bool IsActive {get;set;}


}
