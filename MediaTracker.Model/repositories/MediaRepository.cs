using MediaTracker.Model.entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;

namespace MediaTracker.Model.repositories;

public class MediaRepository : BaseRepository
{
    public MediaRepository(IConfiguration configuration) : base(configuration) { }

    // Get a media item by ID
    public Media GetMediaById(int id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM media_items WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
            var data = GetData(dbConn, cmd);
            if (data != null && data.Read())
            {
                return new Media(Convert.ToInt32(data["id"]))
                {
                    Title = data["title"]?.ToString(),
                    Type = data["type"]?.ToString(),
                    ReleaseYear = Convert.ToInt32(data["releaseyear"]),
                    Rating = data["rating"] as double?,
                    Comments = data["comments"]?.ToString(),
                    StreamingService = data["streamingservice"]?.ToString(),
                    CreatedDate = Convert.ToDateTime(data["created_date"]),
                    UserId = Convert.ToInt32(data["userid"])
                };
            }
            return null;
        }
        finally
        {
            dbConn?.Close();
        }
    }

    // Get all media items
    public List<Media> GetMediaItems()
    {
        NpgsqlConnection dbConn = null;
        var mediaList = new List<Media>();
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM media_items";
            var data = GetData(dbConn, cmd);
            while (data != null && data.Read())
            {
                mediaList.Add(new Media(Convert.ToInt32(data["id"]))
                {
                    Title = data["title"]?.ToString(),
                    Type = data["type"]?.ToString(),
                    ReleaseYear = Convert.ToInt32(data["releaseyear"]),
                    Rating = data["rating"] as double?,
                    Comments = data["comments"]?.ToString(),
                    StreamingService = data["streamingservice"]?.ToString(),
                    CreatedDate = Convert.ToDateTime(data["created_date"]),
                    UserId = Convert.ToInt32(data["userid"])
                });
            }
            return mediaList;
        }
        finally
        {
            dbConn?.Close();
        }
    }

    // Add a new media item
    public bool InsertMedia(Media media)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO media_items (title, type, releaseyear, rating, comments, streamingservice, created_date, userid)
                VALUES (@title, @type, @releaseyear, @rating, @comments, @streamingservice, @created_date, @userid)";
            cmd.Parameters.AddWithValue("@title", NpgsqlDbType.Text, media.Title);
            cmd.Parameters.AddWithValue("@type", NpgsqlDbType.Text, media.Type);
            cmd.Parameters.AddWithValue("@releaseyear", NpgsqlDbType.Integer, media.ReleaseYear);
            cmd.Parameters.AddWithValue("@rating", NpgsqlDbType.Double, (object?)media.Rating ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@comments", NpgsqlDbType.Text, (object?)media.Comments ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@streamingservice", NpgsqlDbType.Text, (object?)media.StreamingService ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@created_date", NpgsqlDbType.Date, media.CreatedDate);
            cmd.Parameters.AddWithValue("@userid", NpgsqlDbType.Integer, media.UserId);
            return InsertData(dbConn, cmd);
        }
        finally
        {
            dbConn?.Close();
        }
    }

    // Update an existing media item
    public bool UpdateMedia(Media media)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                UPDATE media_items
                SET title = @title,
                    type = @type,
                    releaseyear = @releaseyear,
                    rating = @rating,
                    comments = @comments,
                    streamingservice = @streamingservice,
                    userid = @userid
                WHERE id = @id";
            cmd.Parameters.AddWithValue("@title", NpgsqlDbType.Text, media.Title);
            cmd.Parameters.AddWithValue("@type", NpgsqlDbType.Text, media.Type);
            cmd.Parameters.AddWithValue("@releaseyear", NpgsqlDbType.Integer, media.ReleaseYear);
            cmd.Parameters.AddWithValue("@rating", NpgsqlDbType.Double, (object?)media.Rating ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@comments", NpgsqlDbType.Text, (object?)media.Comments ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@streamingservice", NpgsqlDbType.Text, (object?)media.StreamingService ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@userid", NpgsqlDbType.Integer, media.UserId);
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, media.Id);
            return UpdateData(dbConn, cmd);
        }
        finally
        {
            dbConn?.Close();
        }
    }

    // Delete a media item by ID
    public bool DeleteMedia(int id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "DELETE FROM media_items WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
            return DeleteData(dbConn, cmd);
        }
        finally
        {
            dbConn?.Close();
        }
    }
}
