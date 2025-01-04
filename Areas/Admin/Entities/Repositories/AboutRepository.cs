using System;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantPortfolio.Helpers;
using RestaurantPortfolio.Models.Entities;

namespace RestaurantPortfolio.Areas.Admin.Entities.Repositories;

[Area("Admin")]
public class AboutRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ImageHelper _imageHelper;
    public AboutRepository(IDbConnection dbConnection, ImageHelper imageHelper)
    {
        _dbConnection = dbConnection;
        _imageHelper = imageHelper;
    }
    public async Task<IEnumerable<About>> GetAllAsync()
    {
        string query ="SELECT Id,Name,AboutImageUrl FROM Abouts";
        return await _dbConnection.QueryAsync<About>(query);
              
    }

}
