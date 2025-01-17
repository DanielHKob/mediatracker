using MediaTracker.Model.entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;

namespace MediaTracker.Model.repositories;

public class UserRepository : BaseRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration) { }

    // Get a user by ID
    public User GetUserById(int id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM users WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
            var data = GetData(dbConn, cmd);
            if (data != null && data.Read())
            {
                return new User(Convert.ToInt32(data["id"]))
                {
                    FirstName = data["firstname"]?.ToString(),
                    LastName = data["lastname"]?.ToString(),
                    Email = data["email"]?.ToString(),
                    CreatedDate = Convert.ToDateTime(data["created_date"]),
                    Password = data["password"]?.ToString()
                };
            }
            return null;
        }
        finally
        {
            dbConn?.Close();
        }
    }

    public User GetUserByEmail(string email)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM users WHERE email = @email";
            cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Varchar, email);
            var data = GetData(dbConn, cmd);
            if (data != null && data.Read())
            {
                return new User(Convert.ToInt32(data["id"]))
                {
                    FirstName = data["firstname"]?.ToString(),
                    LastName = data["lastname"]?.ToString(),
                    Email = data["email"]?.ToString(),
                    CreatedDate = Convert.ToDateTime(data["created_date"]),
                    Password = data["password"]?.ToString()
                };
            }
            return null;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    
    // Get all users
    public List<User> GetUsers()
    {
        NpgsqlConnection dbConn = null;
        var users = new List<User>();
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM users";
            var data = GetData(dbConn, cmd);
            while (data != null && data.Read())
            {
                users.Add(new User(Convert.ToInt32(data["id"]))
                {
                    FirstName = data["firstname"]?.ToString(),
                    LastName = data["lastname"]?.ToString(),
                    Email = data["email"]?.ToString(),
                    CreatedDate = Convert.ToDateTime(data["created_date"])
                });
            }
            return users;
        }
        finally
        {
            dbConn?.Close();
        }
    }

    // Add a new user
    public bool InsertUser(User user)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO users (firstname, lastname, email, created_date)
                VALUES (@firstname, @lastname, @email, @created_date)";
            cmd.Parameters.AddWithValue("@firstname", NpgsqlDbType.Text, user.FirstName);
            cmd.Parameters.AddWithValue("@lastname", NpgsqlDbType.Text, user.LastName);
            cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Text, user.Email);
            cmd.Parameters.AddWithValue("@created_date", NpgsqlDbType.Date, user.CreatedDate);
            cmd.Parameters.AddWithValue("@password", NpgsqlDbType.Text, user.Password);
            return InsertData(dbConn, cmd);
        }
        finally
        {
            dbConn?.Close();
        }
    }

    // Update an existing user
    public bool UpdateUser(User user)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                UPDATE users
                SET firstname = @firstname,
                    lastname = @lastname,
                    email = @email,
                    password = @password
                WHERE id = @id";
            cmd.Parameters.AddWithValue("@firstname", NpgsqlDbType.Text, user.FirstName);
            cmd.Parameters.AddWithValue("@lastname", NpgsqlDbType.Text, user.LastName);
            cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Text, user.Email);
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, user.Id);
            cmd.Parameters.AddWithValue("@password", NpgsqlDbType.Text, user.Password);

            return UpdateData(dbConn, cmd);
        }
        finally
        {
            dbConn?.Close();
        }
    }

    // Delete a user by ID
    public bool DeleteUser(int id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "DELETE FROM users WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
            return DeleteData(dbConn, cmd);
        }
        finally
        {
            dbConn?.Close();
        }
    }
}
