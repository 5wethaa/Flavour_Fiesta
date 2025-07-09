using Flavour_Fiesta.Data;
using Flavour_Fiesta.DataAccess.Interfaces;
using Flavour_Fiesta.DataAccess.Repositories;
using Flavour_Fiesta.Domain.Interfaces;
using Flavour_Fiesta.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Flavour_Fiesta.DataAccess")   
    ));

builder.Services.AddSession();

// DataAcces Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IFoodItemRepository, FoodItemRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();

// Services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IFoodItemService, FoodItemService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();

var app = builder.Build();
app.UseSession();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


