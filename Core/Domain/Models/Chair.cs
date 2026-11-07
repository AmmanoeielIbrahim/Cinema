using Domain.Enums;

namespace Domain.Models
{
    public class Chair
    {
        public Guid Id { get; set; }
        public int IndexOrder { get; set; }
        public string RowLabel { get; set; } = string.Empty;
        public ChairType Type { get; set; }
        public bool IsActive { get; set; } = true;

        #region Relations

        public Guid HallId { get; set; }
        public Hall Hall { get; set; } = default!;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        #endregion Relations
    }
}