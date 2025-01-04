using System;

namespace RestaurantPortfolio.Models.Entities;

public class Category

{
    public int Id {get;set;}
	public string? Name {get;set;}
	public string? Icon { get; set; }
	public bool IsActive {get;set;}
	public DateTime CreatedDate {get;set;}
	public DateTime UpdatedDate {get;set;}

}
