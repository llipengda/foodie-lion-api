using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.DTO;

public class DishDto
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public string Canteen { get; set; } = null!;

    [Required]
    public string Window { get; set; } = null!;

    public string? Image { get; set; }

    public int FavoriteCount { get; set; } = 0;
}
