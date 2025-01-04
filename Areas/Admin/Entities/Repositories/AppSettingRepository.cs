using System;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantPortfolio.Areas.Admin.ViewModels;
using RestaurantPortfolio.Helpers;

namespace RestaurantPortfolio.Areas.Admin.Entities.Repositories;
[Area("Admin")]

public class AppSettingRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ImageHelper _imageHelper;

    public AppSettingRepository(IDbConnection dbConnection, ImageHelper imageHelper)
    {
        _dbConnection = dbConnection;
        _imageHelper = imageHelper;
    }
    
}
