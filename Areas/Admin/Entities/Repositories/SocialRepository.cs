using System;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantPortfolio.Areas.Admin.ViewModels;

namespace RestaurantPortfolio.Areas.Admin.Entities.Repositories;

[Area("Admin")]
public class SocialRepository
{ 
    private readonly IDbConnection _dbConnection;

    public SocialRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task<IEnumerable<SocialViewModel>> GetAllAsync()
    {
        string query ="SELECT Name,Url,Icon,IsActive FROM Socials";
        return await _dbConnection.QueryAsync<SocialViewModel>(query);
    }
    public async Task<IEnumerable<SocialViewModel>>GetAllAsync(bool isActive)
    {
        string query="SELECT Id,Name,Url,Icon,IsActive FROM Socials WHERE IsActive=@IsActive ";
        return await _dbConnection.QueryAsync<SocialViewModel>(query,new {IsActive=isActive});
    }
    public async Task<SocialViewModel?> GetByIdAsync(int id)
    {
        string query= "SELECT*FROM Socials WHERE Id=@Id ";
        return await _dbConnection.QueryFirstOrDefaultAsync<SocialViewModel?>(query,new {Id=id});
    }
    public async Task CreateAsync(SocialCreateViewModel socialCreateViewModel)
    {
        string query="INSERT INTO Socials(Name,Url,Icon,IsActive) VALUES(@Name,@Url,@Icon,@IsActive)";
        await _dbConnection.ExecuteAsync(query ,socialCreateViewModel);

    }
        public async Task UpdateAsync(SocialUpdateViewModel socialUpdateViewModel)
    {
        string query ="UPDATE Socials SET Name=@Name,Url=@Url, Icon=@Icon, IsActive=@IsActive, UpdatedDate=@UpdatedDate WHERE Id=@Id";
        socialUpdateViewModel.UpdatedDate=DateTime.Now;
        await _dbConnection.ExecuteAsync(query ,socialUpdateViewModel); 
    }
    public async Task DeleteAsync(int id)
    {
        string query="DELETE FROM Socials WHERE Id=@Id";
        await _dbConnection.ExecuteAsync(query , new {Id=id});

    }
     public async Task<bool> ActiveAsync(int id)
    {
        string query="SELECT IsActive FROM Socials WHERE Id=@Id";
        var service = await _dbConnection.QueryFirstOrDefaultAsync(query,new{Id=id});
        bool isActive = !service.IsActive;
        string queryUpdate="UPDATE Socials SET IsActive=@IsActive,UpdatedDate=@UpdatedDate WHERE Id=@Id";

        await _dbConnection.ExecuteAsync(queryUpdate,new {Id=id,IsActive=isActive,UpdatedDate=DateTime.Now});
        return isActive;
    }
}
