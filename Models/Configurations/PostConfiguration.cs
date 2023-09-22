using FoodieLionApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodieLionApi.Models.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserName).HasColumnName("user_name").HasMaxLength(100).IsRequired();
        builder.Property(x => x.Content).HasColumnName("content").HasMaxLength(2000).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.Avatar).HasColumnName("avatar").HasMaxLength(100).IsRequired(false);
    }
}
