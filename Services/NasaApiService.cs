
using System.Text.Json; // Converts NASA JSON into C# objects
using NasaApodGallery.DTOs; // Imports our APOD DTO class

namespace NasaApodGallery.Services
{
    // Service responsible for fetching data from NASA
    public class NasaApiService
    {
        // Used to send HTTP requests to NASA
        private readonly HttpClient _httpClient;

        // Constructor receives HttpClient automatically
        public NasaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetches NASA pictures between two dates
        public async Task<List<ApodDto>> GetApodAsync(
            string startDate,
            string endDate)
        {
            // Convert YYYY-MM-DD into YYMMDD
            string from = DateTime.Parse(startDate).ToString("yyMMdd");
            string to = DateTime.Parse(endDate).ToString("yyMMdd");

            // Build the updated NASA APOD API URL
            string url =
                "https://science.nasa.gov/wp-json/wp/v2/apod-basic" +
                $"?date_from={from}" +
                $"&date_to={to}";

            // Send a GET request and receive the JSON response
            string json = await _httpClient.GetStringAsync(url);

            // Convert the JSON array into a list of APOD DTO objects
            List<ApodDto>? pictures =
                JsonSerializer.Deserialize<List<ApodDto>>(json);

            // Return the pictures, or an empty list if none were found
            return pictures ?? new List<ApodDto>();
        }
    }
}
