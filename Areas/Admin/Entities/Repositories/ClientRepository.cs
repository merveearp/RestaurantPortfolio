using System;
using System.Data;
using AspNetCoreHero.ToastNotification.Abstractions;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantPortfolio.Areas.Admin.ViewModels;
using RestaurantPortfolio.Helpers;
using RestaurantPortfolio.Models.Entities;

namespace RestaurantPortfolio.Areas.Admin.Entities.Repositories;

[Area("Admin")]
public class ClientRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly INotyfService _notyfService;

    public ClientRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task<IEnumerable<Client?>> GetAllAsync()
    {
        string query ="SELECT Id,Name,Comment,ClientImageUrl,SendingTime FROM Clients";
        return await _dbConnection.QueryAsync<Client?>(query);
              
    }
   
}
