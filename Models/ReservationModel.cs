using System;
using System.ComponentModel.DataAnnotations;
using RestaurantPortfolio.Models.Entities;

namespace RestaurantPortfolio.Models;

public class ReservationModel
{
    public AppSetting? AppSetting { get; set; }
    
    [Display(Name ="Ad Soyad")]
    [Required(ErrorMessage = "Ad-Soyad alanı gereklidir.")]
	public string? Name {get;set;}

	[Display(Name ="Email")]
    [Required(ErrorMessage ="Email alanı boş bırakılamaz.")]
	[DataType(DataType.EmailAddress,ErrorMessage ="Email formatında yazınız.")]
	public string? Email {get;set;}

	[Display(Name ="Telefon Numarası")]
	public string? PhoneNumber {get;set;}

	[Display(Name ="Reservasyon Tarihi")]
	public DateTime ReservationDate {get;set;}

	[Display(Name ="Kişi Sayısı")]
    [Required(ErrorMessage = "Kişi sayısı gereklidir.")]
    [Range(1, 12, ErrorMessage = "Kişi sayısı 1 ile 12 arasında olmalıdır.")]
	public int PeopleCount {get;set;}

}
