using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Chair
    {
        public Guid Id { get; set; }
        public Guid HallId { get; set; }
        public int IndexOrder { get; set; }
        public string RowLabel { get; set; } = string.Empty;
        public string Type { get; set; }

        // navigations
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
