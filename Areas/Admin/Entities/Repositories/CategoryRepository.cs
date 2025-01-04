using System;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantPortfolio.Areas.Admin.ViewModels;
using RestaurantPortfolio.Models.Entities;
namespace RestaurantPortfolio.Areas.Admin.Entities.Repositories;

[Area("Admin")]
public class CategoryRepository
{
    private readonly IDbConnection _dbConnection;

    public CategoryRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
    {
        string query ="SELECT Id,Name,Icon,IsActive FROM Categories";
        return await _dbConnection.QueryAsync<CategoryViewModel>(query);

    }
       public async Task<IEnumerable<CategoryViewModel>>GetAllAsync(bool isActive)
    {
        string query="SELECT Id,Name,Icon,IsActive FROM Categories WHERE IsActive=@IsActive ";
        return await _dbConnection.QueryAsync<CategoryViewModel>(query,new {IsActive=isActive});
    }
    public async Task<Category?> GetByIdAsync(int id)
    {
        string query= "SELECT*FROM Categories WHERE Id=@Id ";
        return await _dbConnection.QueryFirstOrDefaultAsync<Category?>(query,new {Id=id});
    }
    public async Task CreateAsync(CategoryCreateViewModel category)
    {
        string query="INSERT INTO Categories(Name,Icon,IsActive) VALUES(@Name,@Icon,@IsActive)";
        await _dbConnection.ExecuteAsync(query ,category);

    }
    public async Task UpdateAsync(CategoryUpdateViewModel category)
    {
        string query ="UPDATE Categories SET Name=@Name, Icon=@Icon, IsActive=@IsActive, UpdatedDate=@UpdatedDate WHERE Id=@Id";
        category.UpdatedDate=DateTime.Now;
        await _dbConnection.ExecuteAsync(query ,category); 
    }
    public async Task DeleteAsync(int id)
    {
        string query="DELETE FROM Categories WHERE Id=@Id";
        await _dbConnection.ExecuteAsync(query , new {Id=id});

    }
    public async Task<bool> ActiveAsync(int id)
    {
        string query="SELECT IsActive FROM Categories WHERE Id=@Id";
        var category = await _dbConnection.QueryFirstOrDefaultAsync(query,new{Id=id});
        bool isActive = !category.IsActive;
        string queryUpdate="UPDATE Categories SET IsActive=@IsActive,UpdatedDate=@UpdatedDate WHERE Id=@Id";

        await _dbConnection.ExecuteAsync(queryUpdate,new {Id=id,IsActive=isActive,UpdatedDate=DateTime.Now});
        return isActive;
    }
    
    
    

}
