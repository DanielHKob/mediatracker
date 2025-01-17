namespace MediaTracker.Model.entities;

public class User
{
         public User(int id)
        {Id = id;}

        // Primary key
        public int Id { get; set; }

        // First name of the user
        public string FirstName { get; set; }

        // Last name of the user
        public string LastName { get; set; }

        // Email of the user
        public string Email { get; set; }

        //Password of the user 
        
        public string Password {get; set;}

        // Date the user record was created (defaulted to the current date)
        public DateTime CreatedDate { get; set; }
}
