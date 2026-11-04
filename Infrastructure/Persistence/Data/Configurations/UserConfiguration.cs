namespace Persistence.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Email).HasMaxLength(150);
            builder.Property(e => e.FirstName).HasMaxLength(50);
            builder.Property(e => e.LastName).HasMaxLength(50);
            builder.Property(e => e.PictureUrl).HasMaxLength(200);
            builder.Property(e => e.PhoneNumber).HasMaxLength(20);
            builder.Property(e => e.Gender).HasConversion(e => e.ToString(), e => Enum.Parse<Gender>(e));
        }
    }
}