using System;

namespace RestaurantPortfolio.Models.Entities;

public class Meal
{
    public int Id {get;set;}
	public string? Name {get;set;}
	public string? Ingredient {get;set;}
	public string? MealImageUrl {get;set;}
	public int CategoryId {get;set;}
	public string? CategoryName {get;set;}
	public int Price {get;set;}
	public bool IsActive {get;set;}
	public DateTime CreatedDate {get;set;}
	public DateTime UpdatedDate {get;set;}

}
