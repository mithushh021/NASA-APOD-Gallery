
using Microsoft.Data.SqlClient; // Provides SQL Server connection classes

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
            // Read connection string from appsettings.json
            _connectionString = configuration.GetConnectionString(
                "DefaultConnection")!;
        }

        // Tests whether SQL Server can be reached
        public async Task<bool> TestConnectionAsync()
        {
            // Create a SQL Server connection
            using SqlConnection connection =
                new SqlConnection(_connectionString);

            // Open the database connection
            await connection.OpenAsync();

            // Return true if connection opened successfully
            return connection.State ==
                System.Data.ConnectionState.Open;
        }
    }
}
