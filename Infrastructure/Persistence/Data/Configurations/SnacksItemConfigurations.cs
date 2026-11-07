namespace Persistence.Data.Configurations
{
    internal class SnacksItemConfigurations : IEntityTypeConfiguration<SnacksItem>
    {
        public void Configure(EntityTypeBuilder<SnacksItem> builder)
        {
            builder.Property(e => e.Description).HasMaxLength(100);
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.PictureUrl).HasMaxLength(150);
            builder.Property(e => e.Price).HasColumnType("decimal(7,2)");
        }
    }
}