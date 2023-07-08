using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;

namespace MultiTracksAPI.Controllers
{
    public class ArtistController : ApiController
    {
        private readonly string connectionString;

        public ArtistController()
        {
            connectionString = @"Server=DESKTOP-A4LD02L\SQLEXPRESS;Database=multiTracksdb01;Integrated Security=True";
        }

        [HttpGet]
        [Route("api.multitracks.com/artist/search")]
        public IHttpActionResult Search(string artistName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Artist WHERE Name LIKE '%' + @ArtistName + '%'";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ArtistName", artistName);

                    List<Artist> artists = new List<Artist>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Artist artist = new Artist
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                             
                            };

                            artists.Add(artist);
                        }
                    }

                    if (artists.Count > 0)
                    {
                        return Ok(artists);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api.multitracks.com/song/list")]
        public IHttpActionResult GetSongs(int pageSize = 10, int pageNumber = 1)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT TOP (@PageSize) * FROM Songs ORDER BY Id OFFSET @Offset ROWS";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PageSize", pageSize);
                    command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);

                    List<Song> songs = new List<Song>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Song song = new Song
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = reader["Title"].ToString(),
                                
                            };

                            songs.Add(song);
                        }
                    }

                    return Ok(songs);
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("api.multitracks.com/artist/add")]
        public IHttpActionResult AddArtist([FromBody] Artist artist)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Artist (Name) VALUES (@Name)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", artist.Name);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return InternalServerError();
                    }
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }


    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
    }

    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
    }
}
