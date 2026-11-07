namespace Persistence.Data.Configurations
{
    internal class ChairsConfigurations : IEntityTypeConfiguration<Chair>
    {
        public void Configure(EntityTypeBuilder<Chair> builder)
        {
            builder.HasOne(e => e.Hall).WithMany(e => e.Chairs);
            builder.Property(e => e.RowLabel).HasMaxLength(30);
            builder.HasIndex(e => new { e.RowLabel, e.IndexOrder }).IsUnique();
            builder.Property(e => e.Type).HasConversion<string>();
        }
    }
}