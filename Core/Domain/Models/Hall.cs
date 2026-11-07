namespace Domain.Models
{
    public class Hall
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int IndexOrder { get; set; }
        public bool IsActive { get; set; } = true;

        #region Relations

        public ICollection<Chair> Chairs { get; set; } = [];
        public ICollection<Show> Shows { get; set; } = [];

        #endregion Relations
    }
}