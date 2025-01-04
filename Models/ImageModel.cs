using System;

namespace RestaurantPortfolio.Models;

public class ImageModel
{
    public string? ImageUrl { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }

}
