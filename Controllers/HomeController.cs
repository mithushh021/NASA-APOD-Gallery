
using Microsoft.AspNetCore.Mvc; // Provides MVC controller functionality
using NasaApodGallery.Services; // Imports our NASA API service
using NasaApodGallery.DTOs; // Imports our NASA DTO class

namespace NasaApodGallery.Controllers
{
    // Handles requests to the home page
    public class HomeController : Controller
    {
        // Stores our NASA API service
        private readonly NasaApiService _nasaService;

        // Constructor receives the NASA service automatically
        public HomeController(NasaApiService nasaService)
        {
            _nasaService = nasaService;
        }

        // Handles requests to the home page
        public async Task<IActionResult> Index()
        {
            // Starting date for NASA pictures
            string startDate = "2026-10-01";

            // Ending date for NASA pictures
            string endDate = "2026-10-05";

            // Fetch NASA APOD data for the selected dates
            List<ApodDto> pictures =
                await _nasaService.GetApodAsync(startDate, endDate);

            // Send the NASA data to our HTML view
            return View(pictures);
        }
    }
}
