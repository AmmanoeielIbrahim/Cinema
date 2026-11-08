namespace Persistence.Data.Configurations
{
    internal class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasOne(e => e.User).WithMany(e => e.Bookings).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Show).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasAlternateKey(e => new { e.Id, e.ShowId });
            builder.Property(e => e.Status).HasConversion<string>();
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}