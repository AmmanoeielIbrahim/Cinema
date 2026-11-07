namespace Persistence.Data.Configurations
{
    internal class BookingSnacksConfigurations : IEntityTypeConfiguration<BookingSnack>
    {
        public void Configure(EntityTypeBuilder<BookingSnack> builder)
        {
            builder.HasKey(e => new { e.BookingId, e.SnacksItemId });
            builder.HasOne(e => e.Booking).WithMany(e => e.BookingSnacks);
            builder.HasOne(e => e.SnacksItem).WithMany(e => e.BookingSnacks);
        }
    }
}