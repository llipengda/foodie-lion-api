using FoodieLionApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodieLionApi.Models.Configurations;

public class DishConfiguration : IEntityTypeConfiguration<Dish>
{
    public void Configure(EntityTypeBuilder<Dish> builder)
    {
        builder.ToTable("dishes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(x => x.Window).HasColumnName("window").HasMaxLength(100).IsRequired();
        builder.Property(x => x.Canteen).HasColumnName("canteen").HasMaxLength(100).IsRequired();
        builder.Property(x => x.Image).HasColumnName("image").HasMaxLength(200).IsRequired(false);
        builder
            .Property(x => x.FavoriteCount)
            .HasColumnName("favorite_count")
            .IsRequired()
            .HasDefaultValue(0);
        builder
            .Property(x => x.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(10,3)")
            .IsRequired();
    }
}
