
using Microsoft.AspNetCore.Mvc; // MVC functionality
using NasaApodGallery.Data; // ADO.NET repository
using NasaApodGallery.Models; // APOD database model

namespace NasaApodGallery.Controllers
{
    public class HomeController : Controller
    {
        // Stores SQL Server repository
        private readonly ApodRepository _repository;

        // Constructor receives repository
        public HomeController(ApodRepository repository)
        {
            _repository = repository;
        }

        // Displays saved NASA pictures
        public async Task<IActionResult> Index()
        {
            // Retrieve saved pictures from SQL Server
            List<Apod> pictures =
                await _repository.GetAllApodsAsync();

            // Send pictures to the view
            return View(pictures);
        }
    }
}
