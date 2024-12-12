namespace CinemaTix.Models
{
    public class Movies : BaseProperty
    {
        public required string Title { get; set; }
        public string? Synopsis { get; set; }
        public int Duration { get; set; } // in minutes
        public double Rating { get; set; } // 1 - 5
        public string? PosterImageUrl { get; set; } // will save the url path
    }
}
