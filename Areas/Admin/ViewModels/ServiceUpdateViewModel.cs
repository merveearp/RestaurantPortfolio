using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantPortfolio.Areas.Admin.ViewModels;

public class ServiceUpdateViewModel
{
    public int Id {get;set;}

    [Display(Name ="Hizmet")]
    [Required(ErrorMessage ="Hizmet bilgilendirmesi yapmalısınız.")]
	public string? Title {get;set;}

    [Display(Name ="Açıklama")]   
	public string? Description {get;set;}

    [Display(Name ="Ikon")]
	public string? Icon {get;set;}
    public List<SelectListItem>? IconList { get; set; }

    [Display(Name ="Aktif")]
	public bool IsActive {get;set;}
	public DateTime? UpdatedDate {get;set;}

}
