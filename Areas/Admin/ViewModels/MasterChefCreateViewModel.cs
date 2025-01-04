using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantPortfolio.Areas.Admin.ViewModels;

public class MasterChefCreateViewModel
{
    [Display(Name ="Şefin İsim")]
    [Required(ErrorMessage ="İsim boş bırakılmamalıdır.")]
	public string? Name {get;set;}

     [Display(Name ="Şef Bilgisi")]
    [Required(ErrorMessage ="Açıklama boş bırakılmamalıdır.")]
	public string? ChefText {get;set;}

     [Display(Name ="Şef Profil Resmi")]
    [Required(ErrorMessage ="İsim boş bırakılmamalıdır.")]
    public IFormFile? Image { get; set; }
	public string? MasterChefsUrl {get;set;}
    
    [Display(Name ="Aktif")]
	public bool IsActive {get;set;}
	public DateTime UpdatedDate {get;set;}

}
