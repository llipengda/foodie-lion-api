using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.DTO;

public class WindowDto
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Canteen { get; set; } = null!;

    public string? Image { get; set; }

    public int FavoriteCount { get; set; } = 0;
}
