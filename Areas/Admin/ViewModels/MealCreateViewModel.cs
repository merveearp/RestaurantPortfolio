using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantPortfolio.Areas.Admin.ViewModels;

public class MealCreateViewModel
{
    [Display(Name ="Yemek")]
    [Required(ErrorMessage ="İsim belirtilmelidir.")]

	public string? Name {get;set;}

    [Display(Name ="Yemek İçeriği")]
    [Required(ErrorMessage ="İçerik Bilgilendirmesi yapmalısınız.")]

	public string? Ingredient {get;set;}

    [Display(Name ="Yemek Görseli")]
	public string? MealImageUrl {get;set;}
    public IFormFile? Image { get; set; }
	public int CategoryId {get;set;}
	public string? CategoryName {get;set;}

    [Display(Name ="Yemek Kategorisi")]
    public List<SelectListItem>? CategoryList { get; set; }
   
    [Display(Name ="Fiyat")]
    [Required(ErrorMessage ="Fiyat bilgisi eklenmelidir.")]

	public int Price {get;set;}

    [Display(Name ="Aktif")]
	public bool IsActive {get;set;}
}
