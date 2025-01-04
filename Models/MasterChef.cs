using System;

namespace RestaurantPortfolio.Models.Entities;

public class MasterChef

{
    public int Id {get;set;}
	public string? Name {get;set;}
	public string? ChefText {get;set;}
	public string? MasterChefsUrl {get;set;}
	public bool IsActive {get;set;}
	public DateTime CreatedDate {get;set;}
	public DateTime UpdatedDate {get;set;}


}
