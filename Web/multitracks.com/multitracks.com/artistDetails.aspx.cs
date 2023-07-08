
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
                            .GroupBy(row => row.Field<string>("AlbumTitle")) // Group by album title
                            .Select(group => group.First()) // Select the first row from each group (distinct albums)
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
}















































