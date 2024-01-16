using ShopPriceComparer.Core.Factories.Selenium;
using ShopPriceComparer.Core.Models;
using ShopPriceComparer.Core.Interfaces;
using ShopPriceComparer.Core.Services;
using ShopPriceComparer.Core.Factories;
using Microsoft.Extensions.Options;
using ShopPriceComparer.WebUI.MappingProfiles;
using OpenQA.Selenium;



var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddOptions()
    .Configure<SessionSettings>(builder.Configuration.GetSection("SessionSettings"))
    .AddSingleton<INamedBrowserFactory, ChromeFactory>()
    .AddSingleton<INamedBrowserFactory, FirefoxFactory>()
    .AddTransient(s => new WebDriverFactory(s, s.GetRequiredService<IOptions<SessionSettings>>()).Create());
    


builder.Services.AddScoped<IScraper, MediaExpertScraper>();
builder.Services.AddScoped<IScraper, MoreleScraper>();
builder.Services.AddScoped<ScraperFactory>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ProductMappingProfile>();
},typeof(Program));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var app = builder.Build();
var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
var webDriverFactory = app.Services.GetRequiredService<IWebDriver>();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Products/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");
app.Use(async (context, next) =>
{
    lifetime.ApplicationStopping.Register(() =>
    {
        webDriverFactory.Quit();
    });

    await next();
});

app.Run();
