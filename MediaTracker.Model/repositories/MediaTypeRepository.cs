using MediaTracker.Model.entities;
using MediaTracker.Model.repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;

public class MediaTypeRepository:BaseRepository
{
    public MediaTypeRepository(IConfiguration configuration) : base(configuration) { }

    // Get a media type by ID
    public MediaType GetMediaTypeById(int id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM media_type WHERE mediatype_id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
            var data = GetData(dbConn, cmd);

            if (data != null && data.Read())
            {
                return new MediaType(Convert.ToInt32(data["mediatype_id"]))
                {
                    MediaTypeName = data["mediatype_name"].ToString(),
                    CreatedDate = Convert.ToDateTime(data["created_date"])
                };
            }
            return null;
        }
        finally
        {
            dbConn?.Close();
        }
    }

    // Get all media types
    public List<MediaType> GetMediaTypes()
    {
        NpgsqlConnection dbConn = null;
        var mediaTypes = new List<MediaType>();
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM media_type";
            var data = GetData(dbConn, cmd);

            while (data != null && data.Read())
            {
                mediaTypes.Add(new MediaType(Convert.ToInt32(data["mediatype_id"]))
                {
                    MediaTypeName = data["mediatype_name"].ToString(),
                    CreatedDate = Convert.ToDateTime(data["created_date"])
                });
            }
            return mediaTypes;
        }
        finally
        {
            dbConn?.Close();
        }
    }

    // Add a new media type
    public bool InsertMediaType(MediaType mediaType)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO media_type (mediatype_name, created_date)
                VALUES (@mediatype_name, @created_date)";
            cmd.Parameters.AddWithValue("@mediatype_name", NpgsqlDbType.Text, mediaType.MediaTypeName);
            cmd.Parameters.AddWithValue("@created_date", NpgsqlDbType.Timestamp, mediaType.CreatedDate);
            return InsertData(dbConn, cmd);
        }
        finally
        {
            dbConn?.Close();
        }
    }

    // Update an existing media type
    public bool UpdateMediaType(MediaType mediaType)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                UPDATE media_type
                SET mediatype_name = @mediatype_name
                WHERE mediatype_id = @id";
            cmd.Parameters.AddWithValue("@mediatype_name", NpgsqlDbType.Text, mediaType.MediaTypeName);
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, mediaType.MediaTypeId);
            return UpdateData(dbConn, cmd);
        }
        finally
        {
            dbConn?.Close();
        }
    }

    // Delete a media type by ID
    public bool DeleteMediaType(int id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "DELETE FROM media_type WHERE mediatype_id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
            return DeleteData(dbConn, cmd);
        }
        finally
        {
            dbConn?.Close();
        }
    }
}
