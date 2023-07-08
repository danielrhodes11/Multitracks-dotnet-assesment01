CREATE TABLE dbo.Song
(
	songID INT PRIMARY KEY IDENTITY (1, 1),
	dateCreation SMALLDATETIME NOT NULL DEFAULT GETDATE(),
	albumID INT NOT NULL,
	artistID INT NOT NULL,
	title VARCHAR(255) NOT NULL,
	bpm DECIMAL(6, 2) NOT NULL, 
	timeSignature VARCHAR(10) NOT NULL,
	multitracks BIT NOT NULL,
	customMix BIT NOT NULL,
	chart BIT NOT NULL,
	rehearsalMix BIT NOT NULL,
	patches BIT NOT NULL,
	songSpecificPatches BIT NOT NULL,
	proPresenter BIT NOT NULL
)
