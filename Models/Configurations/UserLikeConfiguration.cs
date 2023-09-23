using FoodieLionApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodieLionApi.Models.Configurations;

public class UserLikeConfiguration : IEntityTypeConfiguration<UserLike>
{
    public void Configure(EntityTypeBuilder<UserLike> builder)
    {
        builder.ToTable("user_likes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserName).HasColumnName("user_name").HasMaxLength(200).IsRequired();
        builder
            .Property(x => x.LikedName)
            .HasColumnName("liked_name")
            .HasMaxLength(200)
            .IsRequired();
    }
}
