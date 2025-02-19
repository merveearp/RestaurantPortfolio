using System;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantPortfolio.Areas.Admin.ViewModels;
using RestaurantPortfolio.Helpers;
using RestaurantPortfolio.Models.Entities;

namespace RestaurantPortfolio.Areas.Admin.Entities.Repositories;
[Area("Admin")]
public class MealRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ImageHelper _imageHelper;

    public MealRepository(IDbConnection dbConnection, ImageHelper imageHelper)
    {
        _dbConnection = dbConnection;
        _imageHelper = imageHelper;
    }
    public async Task<IEnumerable<MealViewModel>> GetAllAsync()
    {
        string query=@"
        SELECT 
        m.Id,
        m.Name,
        c.Name AS CategoryName,
        m.Ingredient,
        m.MealImageUrl,
        m.Price,
        m.IsActive
         FROM Meals m JOIN Categories c
            ON m.CategoryId=c.Id
         ";
        return await _dbConnection.QueryAsync<MealViewModel>(query);
    }
    public async Task<IEnumerable<MealViewModel>> GetAllAsync(bool isActive)
    {
        string query=@"
        SELECT 
        m.Id,
        m.Name,
        c.Name AS CategoryName,
        m.Ingredient,
        m.MealImageUrl,
        m.Price,
        m.IsActive
         FROM Meals m JOIN Categories c
            ON m.CategoryId=c.Id
          WHERE m.IsActive=@IsActive
          ";
        return await _dbConnection.QueryAsync<MealViewModel>(query, new {IsActive=isActive});

    }
    public async Task<Meal?> GetByIdAsync(int id)
    {
        string query=@" SELECT Id,Name,CategoryName,Ingredient,MealImageUrl,Price,IsActive FROM Meals WHERE Id=@Id ";
        return await _dbConnection.QueryFirstOrDefaultAsync<Meal?>(query , new{Id=id});

    }
    public async Task CreateAsync(MealCreateViewModel meal)
    {
        string query ="INSERT INTO Meals (Name,Ingredient,CategoryId,CategoryName,MealImageUrl,Price,IsActive) VALUES (@Name,@Ingredient,@CategoryId,@CategoryName,@MealImageUrl,@Price,@IsActive) ";
        await _dbConnection.ExecuteAsync(query,meal);
    }
    public async Task UpdateAsync(MealUpdateViewModel meal)
    {
        string query=@"
        UPDATE Meals 
        SET 
        Name=@Name,
        Ingredient=@Ingredient,
        CategoryId=@CategoryId,
        MealImageUrl=@MealImageUrl,
        Price=@Price,
        UpdatedDate=@UpdatedDate,
        IsActive=@IsActive
        WHERE Id=@Id";
        meal.UpdatedDate=DateTime.Now;
        await _dbConnection.ExecuteAsync(query,meal);
    }
    public async Task DeleteAsync(int id)
    {
        string query="DELETE FROM Meals WHERE Id=@Id";
        await _dbConnection.ExecuteAsync(query , new {Id=id});

    }
    public async Task<bool> ActiveAsync(int id)
    {
        string query="SELECT IsActive FROM Meals WHERE Id=@Id";
        var meal = await _dbConnection.QueryFirstOrDefaultAsync(query,new{Id=id});
        bool isActive = !meal.IsActive;
        string queryUpdate="UPDATE Meals SET IsActive=@IsActive,UpdatedDate=@UpdatedDate WHERE Id=@Id";

        await _dbConnection.ExecuteAsync(queryUpdate,new {Id=id,IsActive=isActive,UpdatedDate=DateTime.Now});
        return isActive;
    }
}