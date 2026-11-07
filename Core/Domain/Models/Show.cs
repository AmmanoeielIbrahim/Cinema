namespace Domain.Models
{
    public class Show
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal EconomyPrice { get; set; }
        public decimal VipPrice { get; set; }

        #region Relations

        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = default!;
        public Guid HallId { get; set; }
        public Hall Hall { get; set; } = default!;

        public ICollection<Ticket> Tickets { get; set; } = [];

        #endregion Relations
    }
}