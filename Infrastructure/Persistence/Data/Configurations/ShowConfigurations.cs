namespace Persistence.Data.Configurations
{
    internal class ShowConfigurations : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder.HasOne(e => e.Movie).WithMany(e => e.Shows);
            builder.HasOne(e => e.Hall).WithMany(e => e.Shows);
            builder.Property(e => e.EconomyPrice).HasColumnType("decimal(7,2)");
            builder.Property(e => e.VipPrice).HasColumnType("decimal(7,2)");
        }
    }
}