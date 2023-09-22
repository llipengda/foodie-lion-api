using FoodieLionApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodieLionApi.Models.Configurations;

public class EmailCodeConfiguration : IEntityTypeConfiguration<EmailCode>
{
    public void Configure(EntityTypeBuilder<EmailCode> builder)
    {
        builder.ToTable("email_codes");
        builder.HasKey(emailCode => emailCode.Id);
        builder
            .Property(emailCode => emailCode.Email)
            .HasColumnName("email")
            .HasMaxLength(100)
            .IsRequired();
        builder
            .Property(emailCode => emailCode.Code)
            .HasColumnName("code")
            .HasMaxLength(10)
            .IsRequired();
        builder
            .Property(emailCode => emailCode.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow);
        builder
            .Property(emailCode => emailCode.ExpiredAt)
            .HasColumnName("expired_at")
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow.AddMinutes(15));
    }
}
