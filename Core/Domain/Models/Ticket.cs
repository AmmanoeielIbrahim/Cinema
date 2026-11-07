namespace Domain.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string TicketCode { get; set; } = string.Empty;

        #region Relations

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public Guid ChairId { get; set; }
        public Chair Chair { get; set; } = default!;
        public Guid ShowId { get; set; }
        public Show Show { get; set; } = default!;
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = default!;

        #endregion Relations
    }
}