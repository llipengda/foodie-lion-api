using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodieLionApi.Models.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(user => user.Id);
            builder
                .Property(user => user.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(user => user.Email)
                .HasColumnName("email")
                .HasMaxLength(50)
                .IsRequired();
            builder
                .Property(user => user.Password)
                .HasColumnName("password")
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(user => user.Role)
                .HasColumnName("role")
                .HasMaxLength(10)
                .HasConversion<string>()
                .IsRequired()
                .HasDefaultValue(UserRole.USER);
            builder
                .Property(user => user.Avatar)
                .HasColumnName("avatar")
                .HasMaxLength(200)
                .IsRequired(false);
            builder
                .Property(user => user.Signature)
                .HasColumnName("signature")
                .HasMaxLength(1000)
                .IsRequired(false);
        }
    }
}
