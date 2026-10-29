
using Microsoft.Data.SqlClient;
using NasaApodGallery.DTOs;
using NasaApodGallery.Models; // Imports the Apod model

namespace NasaApodGallery.Data
{
    // Handles SQL Server database operations
    public class ApodRepository
    {
        // Stores the SQL Server connection string
        private readonly string _connectionString;

        // Constructor receives application configuration
        public ApodRepository(IConfiguration configuration)
        {
            // Read the connection string from appsettings.json
            _connectionString = configuration.GetConnectionString(
                "DefaultConnection")!;
        }

        // Tests the SQL Server connection
        public async Task<bool> TestConnectionAsync()
        {
            // Create a SQL Server connection
            using SqlConnection connection =
                new SqlConnection(_connectionString);

            // Open the connection
            await connection.OpenAsync();

            // Check whether the connection is open
            return connection.State ==
                System.Data.ConnectionState.Open;
        }

        // Saves a NASA picture if its date does not already exist
        public async Task SaveApodAsync(ApodDto picture)

        {
            // Create the SQL Server connection
            using SqlConnection connection =
                new SqlConnection(_connectionString);

            // Open the connection
            await connection.OpenAsync();

            // SQL query to insert a new picture
            string sql = @"
                INSERT INTO Apod
                    ([Date], Title, Explanation, [Url],
                     MediaType, ServiceVersion)
                SELECT
                    @Date, @Title, @Explanation, @Url,
                    @MediaType, @ServiceVersion
                WHERE NOT EXISTS
                (
                    SELECT 1 FROM Apod WHERE [Date] = @Date
                )";

            // Create the SQL command
            using SqlCommand command =
                new SqlCommand(sql, connection);

            // Pass values safely using SQL parameters
            command.Parameters.AddWithValue(
                "@Date", DateTime.Parse(picture.Date));

            command.Parameters.AddWithValue(
                "@Title", picture.Title);

            command.Parameters.AddWithValue(
                "@Explanation", picture.Explanation);

            // Store the actual astronomy image URL
            command.Parameters.AddWithValue(
                "@Url", picture.MediaType == "image"
                    && !string.IsNullOrWhiteSpace(picture.Hdurl)
                    ? picture.Hdurl
                    : picture.Url);

            command.Parameters.AddWithValue(
                "@MediaType", picture.MediaType);

            command.Parameters.AddWithValue(
                "@ServiceVersion",
                string.IsNullOrWhiteSpace(picture.ServiceVersion)
                    ? DBNull.Value
                    : picture.ServiceVersion);

            // Execute the INSERT query
            await command.ExecuteNonQueryAsync();
        }

        
        // Retrieves all saved NASA pictures from SQL Server
        public async Task<List<Apod>> GetAllApodsAsync()
        {
            // Create a list to store the pictures
            List<Apod> pictures = new List<Apod>();

            // Create the SQL Server connection
            using SqlConnection connection =
                new SqlConnection(_connectionString);

            // Open the connection
            await connection.OpenAsync();

            // Retrieve saved pictures, newest first
            string sql = @"
                SELECT Id, [Date], Title, Explanation,
                       [Url], MediaType, ServiceVersion, SavedAt
                FROM Apod
                ORDER BY [Date] DESC";

            // Create the SQL command
            using SqlCommand command =
                new SqlCommand(sql, connection);

            // Execute the query and read the results
            using SqlDataReader reader =
                await command.ExecuteReaderAsync();

            // Read each database record
            while (await reader.ReadAsync())
            {
                // Convert the database row into an Apod object
                Apod picture = new Apod
                {
                    Id = reader.GetInt32(0),
                    Date = reader.GetDateTime(1),
                    Title = reader.GetString(2),
                    Explanation = reader.IsDBNull(3)
                        ? ""
                        : reader.GetString(3),
                    Url = reader.GetString(4),
                    MediaType = reader.GetString(5),
                    ServiceVersion = reader.IsDBNull(6)
                        ? ""
                        : reader.GetString(6),
                    SavedAt = reader.GetDateTime(7)
                };

                // Add the picture to our list
                pictures.Add(picture);
            }

            // Return all saved pictures
            return pictures;
        }

    }

    
}
