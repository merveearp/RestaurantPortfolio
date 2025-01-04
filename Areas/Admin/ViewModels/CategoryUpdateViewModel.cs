using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantPortfolio.Areas.Admin.ViewModels;

public class CategoryUpdateViewModel
{
    public int Id {get;set;}

    [Display(Name ="Kategori")]
	[Required(ErrorMessage ="Bu alan zorunlu alandır")]
	public string? Name {get;set;}

    [Display(Name ="Icon")]
	public string? Icon { get; set; }
    public List<SelectListItem>? IconList { get; set; }
	public DateTime? UpdatedDate {get;set;}

    [Display(Name ="Aktif")]
	public bool IsActive {get;set;}



}
