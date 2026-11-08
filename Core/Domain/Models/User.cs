using Microsoft.AspNetCore.Identity;
using Share.Enums;

namespace Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PictureUrl { get; set; } = default!;
        public DateOnly BirthDate { get; set; }
        public Gender Gender { get; set; }

        #region Relations

        public ICollection<Booking> Bookings { get; set; } = [];

        #endregion Relations
    }
}