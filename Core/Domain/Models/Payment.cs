using Share.Enums;

namespace Domain.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime? PaidAt { get; set; }

        #region Relations

        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = default!;
        public ICollection<Ticket> Tickets { get; set; } = [];

        #endregion Relations
    }
}