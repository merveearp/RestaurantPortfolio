using System;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantPortfolio.Areas.Admin.ViewModels;
using RestaurantPortfolio.Models.Entities;

namespace RestaurantPortfolio.Areas.Admin.Entities.Repositories;

[Area("Admin")]

public class ServiceRepository
{
    private readonly IDbConnection _dbConnection;

    public ServiceRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task<IEnumerable<ServiceViewModel>> GetAllAsync()
    {
        string query ="SELECT Title,Description,Icon,IsActive FROM Services";
        return await _dbConnection.QueryAsync<ServiceViewModel>(query);

    }
       public async Task<IEnumerable<ServiceViewModel>>GetAllAsync(bool isActive)
    {
        string query="SELECT Id,Title,Description,Icon,IsActive FROM Services WHERE IsActive=@IsActive ";
        return await _dbConnection.QueryAsync<ServiceViewModel>(query,new {IsActive=isActive});
    }
    public async Task<Service?> GetByIdAsync(int id)
    {
        string query= "SELECT*FROM Services WHERE Id=@Id ";
        return await _dbConnection.QueryFirstOrDefaultAsync<Service?>(query,new {Id=id});
    }
    public async Task CreateAsync(ServiceCreateViewModel serviceCreateViewModel)
    {
        string query="INSERT INTO Services(Title,Description,Icon,IsActive) VALUES(@Title,@Description,@Icon,@IsActive)";
        await _dbConnection.ExecuteAsync(query ,serviceCreateViewModel);

    }
        public async Task UpdateAsync(ServiceUpdateViewModel serviceUpdateViewModel)
    {
        string query ="UPDATE Services SET Title=@Title,Description=@Description, Icon=@Icon, IsActive=@IsActive, UpdatedDate=@UpdatedDate WHERE Id=@Id";
        serviceUpdateViewModel.UpdatedDate=DateTime.Now;
        await _dbConnection.ExecuteAsync(query ,serviceUpdateViewModel); 
    }
    public async Task DeleteAsync(int id)
    {
        string query="DELETE FROM Services WHERE Id=@Id";
        await _dbConnection.ExecuteAsync(query , new {Id=id});

    }
    public async Task<bool> ActiveAsync(int id)
    {
        string query="SELECT IsActive FROM Services WHERE Id=@Id";
        var service = await _dbConnection.QueryFirstOrDefaultAsync(query,new{Id=id});
        bool isActive = !service.IsActive;
        string queryUpdate="UPDATE Services SET IsActive=@IsActive,UpdatedDate=@UpdatedDate WHERE Id=@Id";

        await _dbConnection.ExecuteAsync(queryUpdate,new {Id=id,IsActive=isActive,UpdatedDate=DateTime.Now});
        return isActive;
    }

}
