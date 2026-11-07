namespace Domain.Models
{
    public class BookingSnack
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        #region Relations

        public ICollection<Payment> Payments { get; set; } = [];
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = default!;
        public Guid SnacksItemId { get; set; }
        public SnacksItem SnacksItem { get; set; } = default!;

        #endregion Relations
    }
}