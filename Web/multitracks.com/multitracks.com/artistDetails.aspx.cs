
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using DataAccess;

public partial class ArtistDetails : System.Web.UI.Page
{
    //Page_Load: Event handler for the page load event. It retrieves the artist ID from the query string and calls other functions to populate the artist details and bind data to repeater controls.
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
   // RetrieveArtistDetails: Retrieves the artist details from the database based on the artist ID using a stored procedure.Returns the artist details as a DataTable.
    private DataTable RetrieveArtistDetails(string artistID)
    {
        var sql = new SQL();
        sql.Parameters.Add("@artistID", Convert.ToInt32(artistID));
        return sql.ExecuteStoredProcedureDT("GetArtistDetails");
    }
    //PopulateArtistControls: Populates the artist controls on the page with the data from the provided DataRow object.
    private void PopulateArtistControls(DataRow artistData)
    {
        artistImage.ImageUrl = artistData["ArtistImageURL"].ToString();
        artistHeroImage.ImageUrl = artistData["ArtistHeroURL"].ToString();
        artistNameLiteral.Text = artistData["ArtistTitle"].ToString();
        biographyContentLiteral.Text = artistData["ArtistBiography"].ToString();
    }
    //BindRepeater: Binds a DataTable as the data source for a Repeater control.
    private void BindRepeater(Repeater repeater, DataTable dataSource)
    {
        repeater.DataSource = dataSource;
        repeater.DataBind();
    }

}















































