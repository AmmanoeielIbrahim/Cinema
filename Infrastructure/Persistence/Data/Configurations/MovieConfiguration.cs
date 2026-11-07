namespace Persistence.Data.Configurations
{
    internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.Description).HasMaxLength(300);
            builder.Property(e => e.PosterUrl).HasMaxLength(150);
            builder.Property(e => e.VideoPromoUrl).HasMaxLength(150);
            builder.Property(e => e.Status).HasConversion<string>();
        }
    }
}