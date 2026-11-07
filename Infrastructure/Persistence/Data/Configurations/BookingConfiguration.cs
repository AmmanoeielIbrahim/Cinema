namespace Persistence.Data.Configurations
{
    internal class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasOne(e => e.User).WithMany(e => e.Bookings).OnDelete(DeleteBehavior.SetNull);
            builder.Property(e => e.Status).HasConversion<string>();
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}