using System;
using System.Data;
using AspNetCoreHero.ToastNotification.Abstractions;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantPortfolio.Models.Entities;

namespace RestaurantPortfolio.Areas.Admin.Controllers;

[Area("Admin")]
public class ReservationController:Controller
{
    private readonly IDbConnection _dbConnection;
    private readonly INotyfService _notyfService;

    public ReservationController(IDbConnection dbConnection, INotyfService notyfService)
    {
        _dbConnection = dbConnection;
        _notyfService = notyfService;
    }
  public async Task<ActionResult> Index(bool? id = null)
{
    string query;
    bool? isRead = id;

    if (isRead == null)
    {
        query = @"SELECT * FROM Reservations";
    }
    else
    {
        query = @"SELECT * FROM Reservations WHERE IsRead=@IsRead";
    }

    var reservations = await _dbConnection.QueryAsync<Reservation>(query, new { IsRead = isRead });
    ViewBag.IsRead = isRead;
    return View(reservations);
}
  
public async Task<ActionResult> Read(int id)
{
    string query = @"SELECT * FROM Reservations WHERE Id=@Id";
    var reservation = await _dbConnection.QueryFirstOrDefaultAsync<Reservation>(query, new { Id = id });
    if (reservation == null)
    {
        _notyfService.Error("Rezervasyon bulunamadı.");
        return RedirectToAction("Index");
    }

    query = "UPDATE Reservations SET IsRead=@IsRead WHERE Id=@Id";
    await _dbConnection.ExecuteAsync(query, new { IsRead = true, Id = id });

    return View(reservation);
}


// [HttpPost]
public async Task<ActionResult> SetUnread(int id)
{
    string query = @"UPDATE Reservations SET IsRead=@IsRead WHERE Id=@Id";
    await _dbConnection.ExecuteAsync(query, new { Id = id, IsRead = false });
    _notyfService.Success("Rezervasyon okunmadı olarak işaretlendi.");
    return RedirectToAction("Index");
}

}
 
