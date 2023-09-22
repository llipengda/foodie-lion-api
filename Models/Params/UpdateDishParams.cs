namespace FoodieLionApi.Models.Params;

public class UpdateDishParams
{
    public string? Name { get; set; } = null;

    public decimal? Price { get; set; } = null;

    public string? Canteen { get; set; } = null;

    public string? Window { get; set; } = null;

    public string? Image { get; set; } = null;

    public int? FavoriteCount { get; set; } = null;
}
