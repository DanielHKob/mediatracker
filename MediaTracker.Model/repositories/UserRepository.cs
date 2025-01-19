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
                    dateofbirth = Convert.ToDateTime(data["dateofbirth"]),
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
                    dateofbirth = Convert.ToDateTime(data["dateofbirth"]),
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
                    dateofbirth = Convert.ToDateTime(data["dateofbirth"])
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
                INSERT INTO users (firstname, lastname, email, dateofbirth, password)
                VALUES (@firstname, @lastname, @email, @dateofbirth, @password)";
            cmd.Parameters.AddWithValue("@firstname", NpgsqlDbType.Text, user.FirstName);
            cmd.Parameters.AddWithValue("@lastname", NpgsqlDbType.Text, user.LastName);
            cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Text, user.Email);
            cmd.Parameters.AddWithValue("@dateofbirth", NpgsqlDbType.Date, user.dateofbirth);
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

    public int GetLatestUserId()
    {
    using (var dbconn = new NpgsqlConnection(ConnectionString))
    {
        dbconn.Open();
        using (var cmd = dbconn.CreateCommand())
        {
            // Step 1: Get the current value of the sequence
            cmd.CommandText = "SELECT last_value FROM public.user_id_seq";
            var resultObj = cmd.ExecuteScalar();

            // Step 2: Get the maximum user ID from the user table
            cmd.CommandText = "SELECT COALESCE(MAX(id), 0) FROM public.users";
            var maxIdObj = cmd.ExecuteScalar();

            // Convert results to integers
            int sequenceValue = Convert.ToInt32(resultObj);
            int maxUserId = Convert.ToInt32(maxIdObj);

            // Step 3: Compare and update the sequence if necessary
            if (sequenceValue < maxUserId)
            {
                // Update the sequence to match the max user ID
                cmd.CommandText = "SELECT setval('public.user_id_seq', @MaxUserId, true)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("MaxUserId", maxUserId);
                cmd.ExecuteScalar();

                return maxUserId;
            }
            else
            {
                // No update needed; return the sequence value
                return sequenceValue;
            }
        }
    }
}
}
