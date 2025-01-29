namespace MediaTracker.Model.entities;

public class Media-Category
{
    public MediaCategory(int id)
    {
        CategoryId = id;
    }

    // Primary Key
    public int CategoryId { get; set; }

    // Name of the category (e.g., Crime, Comedy)
    public string CategoryName { get; set; }

    // Date the category was created
    public DateTime CreatedDate { get; set; }

}
