using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Persistence.Data.Configurations;

namespace Persistence.Data
{
    public class CinemaDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Payment> Payments;
        public DbSet<Ticket> Tickets;
        public DbSet<BookingSnack> BookingSnacks;
        public DbSet<SnacksItem> SnacksItems;
        public DbSet<Chair> Chairs;
    }
}