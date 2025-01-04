using System;

namespace RestaurantPortfolio.Models;

public class Social
{
    public int Id {get;set;}
	public string? Name {get;set;}
	public string? Url {get;set;}
	public string? Icon {get;set;}
	public bool IsActive {get;set;}
	public DateTime CreatedDate {get;set;}
	public DateTime UpdatedDate {get;set;}


}
