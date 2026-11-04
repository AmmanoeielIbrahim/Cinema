namespace Persistence.Data.Configurations
{
    internal class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATETIME()");
            builder.Property(e => e.Status).HasConversion(e => e.ToString(), e => Enum.Parse<BookingStatus>(e));
            builder.HasOne(e => e.User).WithMany(e => e.Bookings);
        }
    }
}