# NASA APOD Gallery

## Project Overview

NASA APOD Gallery is an ASP.NET Core MVC web application that retrieves astronomy information from NASA's Astronomy Picture of the Day (APOD) API, stores the data in SQL Server, and displays saved astronomy images in a responsive gallery.

## Technologies Used

- ASP.NET Core MVC
- C#
- NASA APOD REST API
- HttpClient
- JSON Deserialization
- ADO.NET (Microsoft.Data.SqlClient)
- SQL Server / SQL Server Express
- HTML, CSS, Razor Views

## Features

- Fetch NASA APOD data for a specified date range.
- Deserialize JSON responses into C# DTO objects.
- Store APOD information in SQL Server using ADO.NET.
- Prevent duplicate APOD records.
- Retrieve saved records from the database.
- Display saved astronomy images and titles in a responsive gallery.

## Prerequisites

Install the following software before running the application:

- .NET SDK
- Visual Studio or Visual Studio Code
- SQL Server or SQL Server Express
- SQL Server Management Studio (SSMS)

## Database Setup

1. Open SQL Server Management Studio.
2. Connect to your SQL Server instance.
3. Open `Database/CreateDatabase.sql`.
4. Execute the SQL script to create the `NasaApodDB` database and `Apod` table.
5. Update the database connection string in `appsettings.json` to match your SQL Server instance.

## NASA API Key Configuration

1. Visit https://api.nasa.gov/.
2. Generate a NASA API key.
3. Open a terminal inside the project folder.
4. Initialize .NET User Secrets if they are not already enabled:

   ```bash
   dotnet user-secrets init
   ```

5. Configure the API key:

   ```bash
   dotnet user-secrets set "NasaApi:ApiKey" "YOUR_NASA_API_KEY"
   ```

**Note:** The configuration key `NasaApi:ApiKey` must match the key used in the application code.

Do not upload your actual NASA API key to GitHub.

## How to Run the Application

1. Open the project in Visual Studio or VS Code.
2. Open a terminal in the project folder.
3. Restore the required dependencies:

   ```bash
   dotnet restore
   ```

4. Start the application:

   ```bash
   dotnet run
   ```

5. Open the local website address displayed in the terminal.

## Application Workflow

1. The application sends a request to NASA's APOD API.
2. NASA returns astronomy information in JSON format.
3. The JSON response is deserialized into DTO objects.
4. New APOD records are inserted into SQL Server using ADO.NET.
5. Saved APOD records are retrieved from SQL Server.
6. The Controller passes the saved records to the Razor View.
7. The View displays astronomy images and titles in a gallery.

## Database Information

**Database Name:** `NasaApodDB`

**Table Name:** `Apod`

The table stores:

- Id
- Date
- Title
- Explanation
- Url
- MediaType
- ServiceVersion
- SavedAt

The Date column has a UNIQUE constraint to prevent duplicate APOD entries.

## Additional Notes

- The application requires an internet connection to fetch data from NASA.
- SQL Server must be running and accessible.
- A valid NASA API key is required.
- Database records remain available after they have been saved.
- The database creation script is intended for a fresh database setup.

## Project Submission

This repository contains the ASP.NET Core MVC application source code, SQL Server database creation script, and instructions required to configure and run the application.