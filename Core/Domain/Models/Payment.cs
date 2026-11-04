using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;

        public DateTime PaidAt { get; set; }

        // Fk
        public Guid BookingId { get; set; }

        // Navigation Property
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<BookingSnack> BookingSnacks { get; set; } = new List<BookingSnack>();

    }
}
