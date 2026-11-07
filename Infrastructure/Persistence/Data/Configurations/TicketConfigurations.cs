namespace Persistence.Data.Configurations
{
    internal class TicketConfigurations : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasOne(e => e.Chair).WithMany(e => e.Tickets).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Booking)
                .WithMany(e => e.Tickets)
                .HasForeignKey(e => new { e.BookingId, e.ShowId })
                .HasPrincipalKey(e => new { e.Id, e.ShowId })
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Show).WithMany(e => e.Tickets).OnDelete(DeleteBehavior.Restrict);
            builder.Property(e => e.Price).HasColumnType("decimal(7,2)");
            builder.Property(e => e.TicketCode).HasMaxLength(50);
            builder.HasIndex(e => new { e.ShowId, e.ChairId }).IsUnique();
            builder.HasIndex(e => e.TicketCode).IsUnique();
        }
    }
}