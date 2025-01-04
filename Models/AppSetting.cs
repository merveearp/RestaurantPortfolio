using System;

namespace RestaurantPortfolio.Models.Entities;

public class AppSetting
{
    public int Id {get;set;}
	public string? BrandName {get;set;}
	public string? HeroTitle {get;set;}
	public string? HeroText {get;set;}
	public string? HeroImageUrl {get;set;}
	public string? AddressText {get;set;}
	public string? AddressDistrict {get;set;}
	public string? AddressProvince {get;set;}
	public string? PhoneNumber {get;set;}
	public string? Email {get;set;}
	public string? AboutSubtitle {get;set;}
	public string? AboutText {get;set;}
	public string? MenuText {get;set;}
	public DateTime CreatedDate {get;set;}
	public DateTime UpdatedDate {get;set;}

}
