using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantPortfolio.Areas.Admin.ViewModels;

public class EditContactViewModel
{
    [Display(Name = "Adres")]
    [Required(ErrorMessage = "Boş bırakılamaz!")]
	public string? AddressText {get;set;}


    [Display(Name = "İlçe")]
    [Required(ErrorMessage = "Boş bırakılamaz!")]
	public string? AddressDistrict {get;set;}


    [Display(Name = "İl")]
    [Required(ErrorMessage = "Boş bırakılamaz!")]
	public string? AddressProvince {get;set;}


    [Display(Name = "Telefon Numarası")]
    [Required(ErrorMessage = "Boş bırakılamaz!")]
	public string? PhoneNumber {get;set;}


    [Display(Name = "Email")]
    [Required(ErrorMessage = "Boş bırakılamaz!")]
	public string? Email {get;set;}

}
