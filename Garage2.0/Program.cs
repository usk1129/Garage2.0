using Garage2._0.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Garage2._0.Data;
using Garage2._0.Extensions;
using Garage2._0.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Garage2_0Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Garage2_0Context")));


builder.Services.AddScoped<IVehicleTypeSelectListService, VehicleTypeSelectListService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddTransient<IParkingSlotRepository, ParkingSlotRepository>();

var app = builder.Build();

//Seed
app.SeedDataAsync().GetAwaiter().GetResult();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ParkVehicles}/{action=Index}/{id?}");

app.Run();
