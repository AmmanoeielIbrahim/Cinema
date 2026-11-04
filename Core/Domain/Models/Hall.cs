namespace Domain.Models
{
    public class Hall
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int IndexOrder { get; set; }

        #region Relations

        public ICollection<Show> Shows { get; set; } = [];

        #endregion Relations
    }
}