using System;

namespace RestaurantPortfolio.Models.Entities;

public class Client
{
    public int Id {get;set;}
	public string? Name {get;set;}
	public string? ClientImageUrl {get;set;}
	public string? Comment {get;set;}
	public bool IsRead {get;set;}
	public DateTime SendingTime {get;set;}

}
