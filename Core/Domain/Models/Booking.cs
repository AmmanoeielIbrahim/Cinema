using Domain.Enums;

namespace Domain.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime ExpiresAt { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }

        #region Relations

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public ICollection<Ticket> Tickets { get; set; } = [];
        public ICollection<BookingSnack> BookingSnacks { get; set; } = [];
        public ICollection<Payment> Payments { get; set; } = [];

        #endregion Relations
    }
}