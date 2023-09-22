using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Params;

namespace FoodieLionApi.Services.Interface;

public interface IDishService
{
    Task<Dish> CreateDishAsync(DishDto dishDto);

    Task<List<Dish>> GetAllDishesAsync(string? order = "favoriteCount", bool? desending = true);

    Task<Dish> GetDishByIdAsync(Guid dishId);

    Task<Dish> UpdateDishAsync(Guid dishId, UpdateDishParams updateDishParams);

    Task<Dish> DeleteDishAsync(Guid dishId);

    Task<int> IncreaseFavoriteCountAsync(Guid dishId);

    Task<List<Dish>> GetDishesByCanteenAsync(string canteen, string? order = "favoriteCount", bool? desending = true);

    Task<List<Dish>> GetDishesByCanteenAndWindowAsync(string canteen, string window, string? order = "favoriteCount", bool? desending = true);

    Task<List<Dish>> GetDishesByNameAsync(string name, string? order = "favoriteCount", bool? desending = true);
}
