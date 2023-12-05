using ConnectToAiWeb.Data;
using MudBlazor.Services;
using DataModel.Utility;
using DataModel.Models;
using Services;
using ConnectToAiWeb.Pages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
#if DEBUG
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
#else
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
#endif
var configuration = builder.Configuration;
var appSettingValues = new AppSettings();
configuration.GetSection("Settings").Bind(appSettingValues);
builder.Services.AddSingleton(appSettingValues);
  
// Add CORS policy 
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        // Allow requests from the specified origin
        builder.WithOrigins("*");
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

builder.Services.AddSingleton<ErrorState>();

builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<DataService>();
builder.Services.AddSingleton<BlobStorageService>();
builder.Services.AddSingleton<OcrService>();
 
builder.Services.AddMudServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseWebSockets();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseCors();

app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");
});

app.Run();