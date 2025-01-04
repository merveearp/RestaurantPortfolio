using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantPortfolio.Areas.Admin.ViewModels;

public class ServiceViewModel
{
    public int Id {get;set;}
	public string? Title {get;set;}
	public string? Description {get;set;}
	public string? Icon {get;set;}
    public List<SelectListItem>? IconList { get; set; }
	public bool IsActive {get;set;}
}
