using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SnacksItem
    {
        public Guid Id { get; set; }
        public Decimal Price { get; set; }
        public String Name { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}
