
using NasaApodGallery.Services; // Imports our NASA API service

// Creates the web application builder
var builder = WebApplication.CreateBuilder(args);

// Adds MVC controllers and views
builder.Services.AddControllersWithViews();

// Registers our NASA API service with HttpClient
builder.Services.AddHttpClient<NasaApiService>();

// Builds the web application
var app = builder.Build();

// Shows error pages outside development mode
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Redirects HTTP requests to HTTPS when configured
app.UseHttpsRedirection();

// Allows CSS, JavaScript, and other static files
app.UseStaticFiles();

// Enables routing
app.UseRouting();

// Enables authorization middleware
app.UseAuthorization();

// Defines the default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Starts the application
app.Run();
