/*
Post-Deployment Script Template
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.
 Use SQLCMD syntax to include a file in the post-deployment script.
 Example:      :r .\myfile.sql
 Use SQLCMD syntax to reference a variable in the post-deployment script.
 Example:      :setvar TableName MyTable
               SELECT * FROM [$(TableName)]
--------------------------------------------------------------------------------------
*/

:r .\Data\"dbo.Artist.Table.sql"

:r .\Data\"dbo.Album.Table.sql"

:r .\Data\"dbo.Song.Table.sql"

:r .\Data\"dbo.AssessmentSteps.Table.sql"
