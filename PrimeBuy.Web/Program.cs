using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PrimeBuy.Application.Helpers;
using PrimeBuy.Application.Profiles;
using PrimeBuy.Application.Services;
using PrimeBuy.Application.Services.Interfaces;
using PrimeBuy.Data;
using PrimeBuy.Data.Repositories;
using PrimeBuy.Data.Repositories.Interfaces;
using PrimeBuy.Entities.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
    
builder.Services.AddIdentity<Customer, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_cookie";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.Cookie.SameSite = SameSiteMode.Unspecified;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrdersProductsRepository, OrdersProductsRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IStripeService, StripeService>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddSingleton<IImageHandler>(provider =>
{
    var webHostEnvironment = provider.GetRequiredService<IWebHostEnvironment>();
    return new ImageHandler(webHostEnvironment);
});
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddAutoMapper(typeof(OrderProfile));
builder.Services.AddControllersWithViews();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
