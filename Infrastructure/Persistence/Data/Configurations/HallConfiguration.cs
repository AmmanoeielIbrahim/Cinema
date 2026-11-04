namespace Persistence.Data.Configurations
{
    internal class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.HasMany(e => e.Shows).WithOne(e => e.Hall).OnDelete(DeleteBehavior.Restrict);
        }
    }
}