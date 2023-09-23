using FoodieLionApi.Models;
using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Models.Params;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodieLionApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DishController : ControllerBase
{
    private readonly IDishService _dishService;

    public DishController(IDishService dishService)
    {
        _dishService = dishService;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result<Dish>>> Create(DishDto dishDto)
    {
        try
        {
            var dish = await _dishService.CreateDishAsync(dishDto);
            return Ok(new Result<Dish> { Code = ErrorCode.SUCCESS, Data = dish });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<Dish> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result<Dish>>> Delete(Guid id)
    {
        try
        {
            var dish = await _dishService.DeleteDishAsync(id);
            return Ok(new Result<Dish> { Code = ErrorCode.SUCCESS, Data = dish });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<Dish> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<Dish>>>> GetAll(
        string? order = "favoriteCount",
        bool? desending = true
    )
    {
        try
        {
            var dishes = await _dishService.GetAllDishesAsync(order, desending);
            return Ok(new Result<List<Dish>> { Code = ErrorCode.SUCCESS, Data = dishes });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<List<Dish>> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<Dish>>> GetById(Guid id)
    {
        try
        {
            var dish = await _dishService.GetDishByIdAsync(id);
            return Ok(new Result<Dish> { Code = ErrorCode.SUCCESS, Data = dish });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<Dish> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result<Dish>>> Update(Guid id, UpdateDishParams updateDishParams)
    {
        try
        {
            var dish = await _dishService.UpdateDishAsync(id, updateDishParams);
            return Ok(new Result<Dish> { Code = ErrorCode.SUCCESS, Data = dish });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<Dish> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpPost("{id}")]
    [Authorize(Roles = "ADMIN,USER")]
    public async Task<ActionResult<Result<int>>> IncreaseFavoriteCount(Guid id)
    {
        try
        {
            var count = await _dishService.IncreaseFavoriteCountAsync(id);
            return Ok(new Result<int> { Code = ErrorCode.SUCCESS, Data = count });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<int> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpPost("{id}")]
    [Authorize(Roles = "ADMIN,USER")]
    public async Task<ActionResult<Result<int>>> DecreaseFavoriteCount(Guid id)
    {
        try
        {
            var count = await _dishService.DecreaseFavoriteCountAsync(id);
            return Ok(new Result<int> { Code = ErrorCode.SUCCESS, Data = count });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<int> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpGet("{canteen}")]
    public async Task<ActionResult<Result<List<Dish>>>> GetByCanteen(
        string canteen,
        string? order = "favoriteCount",
        bool? desending = true
    )
    {
        try
        {
            var dishes = await _dishService.GetDishesByCanteenAsync(canteen, order, desending);
            return Ok(new Result<List<Dish>> { Code = ErrorCode.SUCCESS, Data = dishes });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<List<Dish>> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpGet("{canteen}/{window}")]
    public async Task<ActionResult<Result<List<Dish>>>> GetByCanteenAndWindow(
        string canteen,
        string window,
        string? order = "favoriteCount",
        bool? desending = true
    )
    {
        try
        {
            var dishes = await _dishService.GetDishesByCanteenAndWindowAsync(
                canteen,
                window,
                order,
                desending
            );
            return Ok(new Result<List<Dish>> { Code = ErrorCode.SUCCESS, Data = dishes });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<List<Dish>> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<Result<List<Dish>>>> GetByName(
        string name,
        string? order = "favoriteCount",
        bool? desending = true
    )
    {
        try
        {
            var dishes = await _dishService.GetDishesByNameAsync(name, order, desending);
            return Ok(new Result<List<Dish>> { Code = ErrorCode.SUCCESS, Data = dishes });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<List<Dish>> { Code = ex.Code, Error = ex.Message });
        }
    }
}
