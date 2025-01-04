using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantPortfolio.Areas.Admin.ViewModels;

public class EditGeneralViewModel
{
    [Display(Name = "Site Adı")]
    [Required(ErrorMessage = "Boş bırakılamaz")]
	public string? BrandName {get;set;}
     
    [Display(Name = "Site Sloganı")]
    [Required(ErrorMessage = "Boş bırakılamaz")]
	public string? HeroTitle {get;set;}

     [Display(Name = "Site İçeriği")]
    [Required(ErrorMessage = "Boş bırakılamaz")]
	public string? HeroText {get;set;}
	public string? HeroImageUrl {get;set;}

    [Display(Name = "Site Giriş Görseli")]
    public IFormFile? HeroImage { get; set; }  

    [Display(Name = "Hakkında Başlık")]
	public string? AboutSubtitle {get;set;}

    [Display(Name = "Hakkında Bilgisi")]
	public string? AboutText {get;set;}

    [Display(Name = "Menü Bilgisi")]
	public string? MenuText {get;set;}

}
