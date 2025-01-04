using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantPortfolio.Areas.Admin.ViewModels;

public class SocialCreateViewModel
{
   
    [Display(Name ="Sosyal Medya")]
	public string? Name {get;set;}

    [Display(Name ="URL")]
	public string? Url {get;set;}
	public string? Icon {get;set;}
    public List<SelectListItem>? IconList { get; set; }
    
    [Display(Name ="Aktif")]
	public bool IsActive {get;set;}

}
