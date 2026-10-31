
-- Create a new database
CREATE DATABASE NasaApodDB;
GO

-- Select our database
USE NasaApodDB;
GO


-- Create a table to store NASA APOD data
CREATE TABLE Apod
(
    -- Automatically generated unique ID
    Id INT IDENTITY(1,1) PRIMARY KEY,

    -- Date of NASA's astronomy picture
    [Date] DATE NOT NULL UNIQUE,

    -- Title of the picture
    Title NVARCHAR(300) NOT NULL,

    -- Description of the picture
    Explanation NVARCHAR(MAX) NULL,

    -- Image or video URL
    [Url] NVARCHAR(2000) NOT NULL,

    -- Type of media: image or video
    MediaType NVARCHAR(50) NOT NULL,

    -- NASA API version
    ServiceVersion NVARCHAR(20) NULL,

    -- Date and time when data was saved
    SavedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

-- Display all records
SELECT * FROM Apod;

USE NasaApodDB;

UPDATE Apod
SET Title = 'TEST - Image From SQL Server'
WHERE [Date] = '2026-10-05';

UPDATE Apod
SET Title = 'M104: The Sombrero Galaxy''s Tidal Streams'
WHERE [Date] = '2026-10-05';

