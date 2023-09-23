using FoodieLionApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodieLionApi.Models.Configurations;

public class HomeImageConfiguration : IEntityTypeConfiguration<HomeImage>
{
    public void Configure(EntityTypeBuilder<HomeImage> builder)
    {
        builder.ToTable("home_images");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Url).HasColumnName("url").HasMaxLength(200).IsRequired();
    }
}
