CREATE PROCEDURE dbo.GetArtistDetails
    @artistID INT
AS
BEGIN
    -- Return the artist details and top songs based on the artistID
    SELECT A.artistID,
           A.dateCreation,
           A.title AS ArtistTitle,
           A.biography AS ArtistBiography,
           A.imageURL AS ArtistImageURL,
           A.heroURL AS ArtistHeroURL,
           S.title AS SongTitle,
           S.bpm AS SongBPM,
           S.timeSignature AS TimeSignature,
           AL.imageURL AS AlbumImageURL,
           AL.title AS AlbumTitle
    FROM Artist A
    LEFT JOIN Song S ON A.artistID = S.artistID
    LEFT JOIN Album AL ON S.albumID = AL.albumID
    WHERE A.artistID = @artistID
    ORDER BY S.songID ASC
END;










