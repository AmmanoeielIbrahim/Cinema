namespace Persistence.Data.Configurations
{
    internal class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasOne(e => e.Booking).WithMany(e => e.Payments).OnDelete(DeleteBehavior.SetNull);
            builder.Property(e => e.Status).HasConversion<string>();
            builder.Property(e => e.Amount).HasColumnType("decimal(10,3)");
        }
    }
}