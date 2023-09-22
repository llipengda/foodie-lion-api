using AutoMapper;
using FoodieLionApi.Models;
using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Models.Params;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FoodieLionApi.Services;

public class DishService : IDishService
{
    private readonly FoodieLionDbContext _context;
    private readonly IMapper _mapper;

    public DishService(FoodieLionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Dish> CreateDishAsync(DishDto dishDto)
    {
        if (string.IsNullOrEmpty(dishDto.Name))
        {
            throw new FoodieLionException("Name is required", ErrorCode.INVALID_INPUT);
        }
        if (string.IsNullOrEmpty(dishDto.Canteen))
        {
            throw new FoodieLionException("Canteen is required", ErrorCode.INVALID_INPUT);
        }
        if (string.IsNullOrEmpty(dishDto.Window))
        {
            throw new FoodieLionException("Window is required", ErrorCode.INVALID_INPUT);
        }
        if (
            await _context.Dishes.AnyAsync(
                dish =>
                    dish.Name == dishDto.Name
                    && dish.Window == dishDto.Window
                    && dish.Canteen == dishDto.Canteen
            )
        )
        {
            throw new FoodieLionException("Dish already exists", ErrorCode.DISH_ALREADY_EXISTS);
        }
        if (dishDto.Price < 0)
        {
            throw new FoodieLionException("Price must be positive", ErrorCode.INVALID_INPUT);
        }
        if (dishDto.FavoriteCount < 0)
        {
            throw new FoodieLionException(
                "FavoriteCount must be positive",
                ErrorCode.INVALID_INPUT
            );
        }
        if (
            !await _context.Windows.AnyAsync(
                w => w.Name == dishDto.Window && w.Canteen == dishDto.Canteen
            )
        )
        {
            throw new FoodieLionException("Window does not exist", ErrorCode.WINDOW_NOT_FOUND);
        }
        var dish = _mapper.Map<Dish>(dishDto);
        await _context.Dishes.AddAsync(dish);
        await _context.SaveChangesAsync();
        return dish;
    }

    public async Task<List<Dish>> GetAllDishesAsync(
        string? order = "favoriteCount",
        bool? decending = true
    )
    {
        Expression<Func<Dish, dynamic>> orderBy = order switch
        {
            "favoriteCount" => (d => d.FavoriteCount),
            "price" => (d => d.Price),
            _ => (d => d.FavoriteCount)
        };
        if (decending == true)
        {
            return await _context.Dishes.OrderByDescending(orderBy).ToListAsync();
        }
        else
        {
            return await _context.Dishes.OrderBy(orderBy).ToListAsync();
        }
    }

    public async Task<Dish> GetDishByIdAsync(Guid dishId)
    {
        return await _context.Dishes.FirstOrDefaultAsync(dish => dish.Id == dishId)
            ?? throw new FoodieLionException("Dish not found", ErrorCode.DISH_NOT_FOUND);
    }

    public async Task<Dish> UpdateDishAsync(Guid dishId, UpdateDishParams updateDishParams)
    {
        var dish =
            await _context.Dishes.FirstOrDefaultAsync(dish => dish.Id == dishId)
            ?? throw new FoodieLionException("Dish not found", ErrorCode.DISH_NOT_FOUND);
        if (updateDishParams.Price < 0)
        {
            throw new FoodieLionException("Price must be positive", ErrorCode.INVALID_INPUT);
        }
        if (updateDishParams.FavoriteCount < 0)
        {
            throw new FoodieLionException(
                "FavoriteCount must be positive",
                ErrorCode.INVALID_INPUT
            );
        }
        if (
            !(
                string.IsNullOrEmpty(updateDishParams.Window)
                || await _context.Windows.AnyAsync(w => w.Name == updateDishParams.Window)
            )
            && (
                string.IsNullOrEmpty(updateDishParams.Canteen)
                || await _context.Windows.AnyAsync(w => w.Canteen == updateDishParams.Canteen)
            )
        )
        {
            throw new FoodieLionException("Window does not exist", ErrorCode.WINDOW_NOT_FOUND);
        }
        dish.Update(updateDishParams);
        await _context.SaveChangesAsync();
        return dish;
    }

    public async Task<Dish> DeleteDishAsync(Guid dishId)
    {
        var dish =
            await _context.Dishes.FirstOrDefaultAsync(dish => dish.Id == dishId)
            ?? throw new FoodieLionException("Dish not found", ErrorCode.DISH_NOT_FOUND);
        _context.Dishes.Remove(dish);
        await _context.SaveChangesAsync();
        return dish;
    }

    public async Task<int> IncreaseFavoriteCountAsync(Guid dishId)
    {
        var dish =
            await _context.Dishes.FirstOrDefaultAsync(dish => dish.Id == dishId)
            ?? throw new FoodieLionException("Dish not found", ErrorCode.DISH_NOT_FOUND);
        dish.FavoriteCount++;
        await _context.SaveChangesAsync();
        return dish.FavoriteCount;
    }

    public async Task<List<Dish>> GetDishesByCanteenAsync(
        string canteen,
        string? order = "favoriteCount",
        bool? decending = true
    )
    {
        Expression<Func<Dish, dynamic>> orderBy = order switch
        {
            "favoriteCount" => (d => d.FavoriteCount),
            "price" => (d => d.Price),
            _ => (d => d.FavoriteCount)
        };
        if (decending == true)
        {
            return await _context.Dishes
                .Where(dish => dish.Canteen == canteen)
                .OrderByDescending(orderBy)
                .ToListAsync();
        }
        else
        {
            return await _context.Dishes
                .Where(dish => dish.Canteen == canteen)
                .OrderBy(orderBy)
                .ToListAsync();
        }
    }

    public async Task<List<Dish>> GetDishesByCanteenAndWindowAsync(
        string canteen,
        string window,
        string? order = "favoriteCount",
        bool? decending = true
    )
    {
        Expression<Func<Dish, dynamic>> orderBy = order switch
        {
            "favoriteCount" => (d => d.FavoriteCount),
            "price" => (d => d.Price),
            _ => (d => d.FavoriteCount)
        };
        if (decending == true)
        {
            return await _context.Dishes
                .Where(dish => dish.Canteen == canteen && dish.Window == window)
                .OrderByDescending(orderBy)
                .ToListAsync();
        }
        else
        {
            return await _context.Dishes
                .Where(dish => dish.Canteen == canteen && dish.Window == window)
                .OrderBy(orderBy)
                .ToListAsync();
        }
    }

    public async Task<List<Dish>> GetDishesByNameAsync(
        string name,
        string? order = "favoriteCount",
        bool? decending = true
    )
    {
        Expression<Func<Dish, dynamic>> orderBy = order switch
        {
            "favoriteCount" => (d => d.FavoriteCount),
            "price" => (d => d.Price),
            _ => (d => d.FavoriteCount)
        };
        if (decending == true)
        {
            return await _context.Dishes
                .Where(dish => dish.Name.Contains(name))
                .OrderByDescending(orderBy)
                .ToListAsync();
        }
        else
        {
            return await _context.Dishes
                .Where(dish => dish.Name.Contains(name))
                .OrderBy(orderBy)
                .ToListAsync();
        }
    }
}
