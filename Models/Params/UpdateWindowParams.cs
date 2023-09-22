using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.Params;

public class UpdateWindowParams
{
    public string? Name { get; set; }

    public string? Canteen { get; set; }

    public string? Image { get; set; }

    public int? FavoriteCount { get; set; } = null;
}
