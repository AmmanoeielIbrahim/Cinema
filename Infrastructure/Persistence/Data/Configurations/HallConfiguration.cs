namespace Persistence.Data.Configurations
{
    internal class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(30);
            builder.HasIndex(e => new { e.Name, e.IndexOrder }).IsUnique();
        }
    }
}