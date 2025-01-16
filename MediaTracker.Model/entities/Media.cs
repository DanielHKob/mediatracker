namespace MediaTracker.Model.entities;

public class Media
{
    public Media(int id)
     {Id=id;}
    public int Id {get; set;}

        // Title of the media item
        public string? Title { get; set; }

        // Type of media (e.g., Movie, Series)
        public string? Type { get; set; }

        // Release year of the media item
        public int ReleaseYear { get; set; }

        // Rating of the media item (optional)
        public double? Rating { get; set; }

        // Comments about the media item (optional)
        public string? Comments { get; set; }

        // Streaming service where the media is available (optional)
        public string? StreamingService { get; set; }

        // Date the media item was created (defaulted to the current date)
        public DateTime CreatedDate { get; set; }

        // User ID associated with the media item
        public int UserId { get; set; }

}
