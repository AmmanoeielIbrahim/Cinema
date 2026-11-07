namespace Persistence.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.FirstName).HasMaxLength(50);
            builder.Property(e => e.LastName).HasMaxLength(50);
            builder.HasIndex(e => e.NormalizedEmail).IsUnique();
            builder.Property(e => e.Email).HasMaxLength(100);
            builder.Property(e => e.Gender).HasConversion<string>();
            builder.Property(e => e.PictureUrl).HasMaxLength(200);
        }
    }
}