using ShopPriceComparer.Core.Factories.Selenium;
using ShopPriceComparer.Core.Models;
using ShopPriceComparer.Core.Interfaces;
using ShopPriceComparer.Core.Services;
using ShopPriceComparer.Core.Factories;
using Microsoft.Extensions.Options;
using ShopPriceComparer.WebUI.MappingProfiles;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddOptions()
    .Configure<SessionSettings>(builder.Configuration.GetSection("SessionSettings"))
    .AddSingleton<INamedBrowserFactory, ChromeFactory>() 
    .AddTransient(s => new WebDriverFactory(s, s.GetRequiredService<IOptions<SessionSettings>>()).Create());

builder.Services.AddScoped<IScraper, MediaExpertScraper>();
builder.Services.AddScoped<ScraperFactory>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ProductMappingProfile>();
},typeof(Program));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Products/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();
