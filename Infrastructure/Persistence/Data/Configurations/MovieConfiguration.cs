namespace Persistence.Data.Configurations
{
    internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(20);
            builder.Property(e => e.PosterUrl).HasMaxLength(200);
            builder.Property(e => e.VideoPromoUrl).HasMaxLength(200);
            builder.Property(e => e.Status).HasConversion(e => e.ToString(), e => Enum.Parse<MovieStatus>(e));
            builder.HasMany(e => e.Shows).WithOne(e => e.Movie);
        }
    }
}