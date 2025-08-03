using Flavour_Fiesta.DataAccess;
using Flavour_Fiesta.DataAccess.Data;
using Flavour_Fiesta.DataAccess.Repositories;
using Flavour_Fiesta.Domain.Interfaces;
using Flavour_Fiesta.Service.Services;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); //Enables MVC pattern

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddSession(); 

// DataAcces
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IFoodItemRepository, FoodItemRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();


// Services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IFoodItemService, FoodItemService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();



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


