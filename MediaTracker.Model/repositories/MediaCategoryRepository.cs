using MediaTracker.Model.entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;

namespace MediaTracker.Model.repositories;

public class MediaCategoryRepository: BaseRepository
{
        public MediaCategoryRepository(IConfiguration configuration) : base(configuration) { }

    // Get a category by ID
    public MediaCategory GetMediaCategoryById(int id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM media_category WHERE category_id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
            var data = GetData(dbConn, cmd);

            if (data != null && data.Read())
            {
                return new MediaCategory(Convert.ToInt32(data["category_id"]))
                {
                    CategoryName = data["category_name"].ToString(),
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

    // Get all media categories
    public List<MediaCategory> GetMediaCategories()
    {
        NpgsqlConnection dbConn = null;
        var categories = new List<MediaCategory>();
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM media_category";
            var data = GetData(dbConn, cmd);

            while (data != null && data.Read())
            {
                categories.Add(new MediaCategory(Convert.ToInt32(data["category_id"]))
                {
                    CategoryName = data["category_name"].ToString(),
                    CreatedDate = Convert.ToDateTime(data["created_date"])
                });
            }
            return categories;
        }
        finally
        {
            dbConn?.Close();
        }
    }

    // Add a new media category
    public bool InsertMediaCategory(MediaCategory category)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO media_category (category_name, created_date)
                VALUES (@category_name, @created_date)";
            cmd.Parameters.AddWithValue("@category_name", NpgsqlDbType.Text, category.CategoryName);
            cmd.Parameters.AddWithValue("@created_date", NpgsqlDbType.Timestamp, category.CreatedDate);
            return InsertData(dbConn, cmd);
        }
        finally
        {
            dbConn?.Close();
        }
    }

    // Update an existing media category
    public bool UpdateMediaCategory(MediaCategory category)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                UPDATE media_category
                SET category_name = @category_name
                WHERE category_id = @id";
            cmd.Parameters.AddWithValue("@category_name", NpgsqlDbType.Text, category.CategoryName);
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, category.CategoryId);
            return UpdateData(dbConn, cmd);
        }
        finally
        {
            dbConn?.Close();
        }
    }

    // Delete a media category by ID
    public bool DeleteMediaCategory(int id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "DELETE FROM media_category WHERE category_id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
            return DeleteData(dbConn, cmd);
        }
        finally
        {
            dbConn?.Close();
        }
    }
}
