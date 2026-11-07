namespace Domain.Models
{
    public class SnacksItem
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
        public bool IsAvailable { get; set; }

        #region Relations

        public ICollection<BookingSnack> BookingSnacks { get; set; } = [];

        #endregion Relations
    }
}