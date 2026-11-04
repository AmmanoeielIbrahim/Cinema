using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string TicketCode { get; set; } = string.Empty;
        // Fk
        public Guid BookingId { get; set; }
        public Guid ShowId { get; set; }
        public Guid ChairId { get; set; }

        // navigations
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public Chair? Chair { get; set; }
    }
}
