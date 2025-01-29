namespace MediaTracker.Model.entities;

public class Media-Type
{
    public MediaType(int id)
    {
        MediaTypeId = id;
    }

    // Primary Key
    public int MediaTypeId { get; set; }

    // Name of the media type (e.g., Movie, Series)
    public string MediaTypeName { get; set; }

    // Date the media type was created
    public DateTime CreatedDate { get; set; }
}
