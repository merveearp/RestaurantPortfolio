using System;

namespace RestaurantPortfolio.Models.Entities;

public class About
{
    public int Id {get;set;}
	public string? Name {get;set;}
	public string? AboutImageUrl {get;set;}
	public bool IsActive {get;set;}
	public DateTime CreatedDate {get;set;}
	public DateTime UpdatedDate {get;set;}
}
