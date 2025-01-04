
using System.Data;
using AspNetCoreHero.ToastNotification;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using RestaurantPortfolio.Areas.Admin.Entities.Repositories;
using RestaurantPortfolio.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDbConnection>(o => new SqlConnection(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ServiceRepository>();
builder.Services.AddScoped<SocialRepository>();
builder.Services.AddScoped<MasterChefRepository>();
builder.Services.AddScoped<MealRepository>();
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<AboutRepository>();
builder.Services.AddScoped<AppSettingRepository>();
builder.Services.AddScoped<ImageHelper>();


builder.Services.AddNotyf(Options=>{
    Options.DurationInSeconds=3;
    Options.Position=NotyfPosition.TopRight;
    Options.IsDismissable=true;

});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapAreaControllerRoute(

    name:"adminArea",
    pattern:"admin/{controller=Home}/{action=Index}/{id?}",
    areaName:"Admin"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
