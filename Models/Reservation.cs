using System;

namespace RestaurantPortfolio.Models.Entities;

public class Reservation
{
    public int Id {get;set;}
	public string? Name {get;set;}
	public string? Email {get;set;}
	public string? PhoneNumber {get;set;}
	public DateTime CreatedDate {get;set;}
	public int PeopleCount {get;set;}
	public bool IsActive {get;set;}
	public DateTime UpdatedDate {get;set;}

}
