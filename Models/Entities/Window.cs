using FoodieLionApi.Models.Configurations;
using FoodieLionApi.Models.Params;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.Entities;

[EntityTypeConfiguration(typeof(WindowConfiguration))]
public class Window
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Canteen { get; set; } = null!;

    public string? Image { get; set; }

    public int FavoriteCount { get; set; } = 0;

    public void Update(UpdateWindowParams updateParams)
    {
        if (!string.IsNullOrEmpty(updateParams.Name))
        {
            Name = updateParams.Name;
        }
        if (!string.IsNullOrEmpty(updateParams.Canteen))
        {
            Canteen = updateParams.Canteen;
        }
        if (!string.IsNullOrEmpty(updateParams.Image))
        {
            Image = updateParams.Image;
        }
        if (updateParams.FavoriteCount != null)
        {
            FavoriteCount = updateParams.FavoriteCount.Value;
        }
    }
}
