using System;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantPortfolio.Helpers;
using RestaurantPortfolio.Areas.Admin.ViewModels;
using RestaurantPortfolio.Models.Entities;

namespace RestaurantPortfolio.Areas.Admin.Entities.Repositories;

[Area("Admin")]
public class MasterChefRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ImageHelper _imageHelper;

    public MasterChefRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<MasterChefViewModel>> GetAllAsync()
    {
        string query ="SELECT Id,Name,ChefText,MasterChefsUrl,IsActive FROM MasterChefs";
        return await _dbConnection.QueryAsync<MasterChefViewModel>(query);
              
    }
    public async Task<IEnumerable<MasterChefViewModel>> GetAllAsync(bool isActive)
    {
        string query ="SELECT Id,Name,ChefText,MasterChefsUrl,IsActive FROM MasterChefs WHERE IsActive=@IsActive" ;
        return await _dbConnection.QueryAsync<MasterChefViewModel>(query,new {IsActive=isActive});
    }
    public async Task<MasterChef?> GetByIdAsync(int id)
    {
        string query ="SELECT * FROM MasterChefs WHERE Id=@Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<MasterChef?>(query,new {Id=id} );
    }
    public async Task CreateAsync(MasterChefCreateViewModel masterChef)
    {
        // var imageModel=await _imageHelper.UploadImage(image,"masterchefs");
        // masterChef.MasterChefsUrl=imageModel.ImageUrl;
        string query ="INSERT INTO MasterChefs(Name,ChefText,MasterChefsUrl,IsActive) VALUES (@Name,@ChefText,@MasterChefsUrl,@IsActive)";
        await _dbConnection.ExecuteAsync(query,masterChef);
    }
    public async Task UpdateAsync(MasterChefUpdateViewModel masterChef)
    {
        string query="UPDATE MasterChefs SET Name=@Name,ChefText=@ChefText,MasterChefsUrl=@MasterChefsUrl,IsActive=@IsActive,UpdatedDate=UpdatedDate WHERE Id=@Id";
        masterChef.UpdatedDate=DateTime.Now;
        await _dbConnection.ExecuteAsync(query,masterChef);
    }
    public async Task DeleteAsync(int id)
    {
        string query="DELETE FROM MasterChefs WHERE Id=@Id";
        await _dbConnection.ExecuteAsync(query , new {Id=id});

    }
    public async Task<bool> ActiveAsync(int id)
    {
        string query="SELECT IsActive FROM MasterChefs WHERE Id=@Id";
        var masterchef = await _dbConnection.QueryFirstOrDefaultAsync(query,new{Id=id});
        bool isActive = !masterchef.IsActive;
        string queryUpdate="UPDATE MasterChefs SET IsActive=@IsActive,UpdatedDate=@UpdatedDate WHERE Id=@Id";

        await _dbConnection.ExecuteAsync(queryUpdate,new {Id=id,IsActive=isActive,UpdatedDate=DateTime.Now});
        return isActive;
    }
    
}
