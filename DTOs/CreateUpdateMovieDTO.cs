namespace CinemaTix.DTOs
{
    public class CreateUpdateMovieDTO
    {
        public string? Title { get; set; }
        public string? Synopsis { get; set; }
        public int? Duration { get; set; } // in minutes
        public double? Rating { get; set; } // 0 - 5
        public string? PosterImageUrl { get; set; } // will save the url path

        public CreateUpdateMovieDTO() { }
    }
}
