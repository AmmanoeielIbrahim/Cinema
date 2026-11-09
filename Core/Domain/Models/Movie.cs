using Share.Enums;

namespace Domain.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public string VideoPromoUrl { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DurationMinutes { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public MovieStatus Status { get; set; }

        #region Relations

        public ICollection<Show> Shows { get; set; } = [];

        #endregion Relations
    }
}