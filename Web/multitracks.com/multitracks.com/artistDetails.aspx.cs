
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using DataAccess;

public partial class ArtistDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string artistID = Request.QueryString["artistID"];

            // If no artistID is provided, set a default value
            if (string.IsNullOrEmpty(artistID))
            {
                artistID = "5";  // Default artistID
            }
            try
            {
                DataTable artistDetails = RetrieveArtistDetails(artistID);

                if (artistDetails.Rows.Count > 0)
                {
                    dynamic artistData = artistDetails.Rows[0];

                    if (artistData != null)
                    {
                        PopulateArtistControls(artistData);

                        var topSongs = artistDetails.AsEnumerable()
                            .Where(row => !row.IsNull("SongTitle"))
                            .Take(5)
                            .CopyToDataTable();

                        var artistAlbums = artistDetails.AsEnumerable()
                            .Where(row => !row.IsNull("AlbumTitle"))
                            .GroupBy(row => row.Field<string>("AlbumTitle"))
                            .Select(group => group.First())
                            .Take(8)
                            .CopyToDataTable();

                        BindRepeater(topSongsRepeater, topSongs);
                        BindRepeater(albumsRepeater, artistAlbums);
                    }

                    else
                    {
                        artistDetailsLiteral.Text = "No details found for this artist.";
                        topSongsRepeater.Visible = false;
                        albumsRepeater.Visible = false;
                    }
                }
                else
                {
                    artistDetailsLiteral.Text = "No details found for this artist.";
                    topSongsRepeater.Visible = false;
                    albumsRepeater.Visible = false;
                }
            }
            catch (Exception ex)
            {
                artistDetailsLiteral.Text = "An error occurred while retrieving artist details. Error: " + ex.Message;
                topSongsRepeater.Visible = false;
                albumsRepeater.Visible = false;
            }
        }
    }

    private DataTable RetrieveArtistDetails(string artistID)
    {
        var sql = new SQL();
        sql.Parameters.Add("@artistID", Convert.ToInt32(artistID));
        return sql.ExecuteStoredProcedureDT("GetArtistDetails");
    }

    private void PopulateArtistControls(DataRow artistData)
    {
        artistImage.ImageUrl = artistData["ArtistImageURL"].ToString();
        artistHeroImage.ImageUrl = artistData["ArtistHeroURL"].ToString();
        artistNameLiteral.Text = artistData["ArtistTitle"].ToString();
        biographyContentLiteral.Text = artistData["ArtistBiography"].ToString();
    }

    private void BindRepeater(Repeater repeater, DataTable dataSource)
    {
        repeater.DataSource = dataSource;
        repeater.DataBind();
    }

    private string GetArtistIdByName(string artistName)
    {
        // Query the database to find the artist ID by name
        // For example:
        var sql = new SQL();
        sql.Parameters.Add("@artistName", artistName);
        DataTable result = sql.ExecuteStoredProcedureDT("GetArtistIdByName");

        if (result.Rows.Count > 0)
        {
            // If the artist is found, return the artist ID
            return result.Rows[0]["artistID"].ToString();
        }
        else
        {
            // If no artist is found, return null or a default value
            return null;
        }
    }

}















































