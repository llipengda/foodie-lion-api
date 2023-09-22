using FoodieLionApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodieLionApi.Models.Configurations;

public class WindowConfiguration : IEntityTypeConfiguration<Window>
{
    public void Configure(EntityTypeBuilder<Window> builder)
    {
        builder.ToTable("windows");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(x => x.Canteen).HasColumnName("canteen").HasMaxLength(100).IsRequired();
        builder.Property(x => x.Image).HasColumnName("image").HasMaxLength(200).IsRequired(false);
        builder
            .Property(x => x.FavoriteCount)
            .HasColumnName("favorite_count")
            .IsRequired()
            .HasDefaultValue(0);
    }
}
