using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BookingSnack
    {
        [Key]
        public Guid BookingId { get; set; }
        public Guid SnacksItemId { get; set; }
        public int Quantity { get; set; }
        public Decimal UnitPrice { get; set; }

        // navigation
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
