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
public class WindowController : ControllerBase
{
    private readonly IWindowService _windowService;

    public WindowController(IWindowService windowService)
    {
        _windowService = windowService;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result<Window>>> Create(WindowDto windowDto)
    {
        try
        {
            var window = await _windowService.CreateWindowAsync(windowDto);
            return Ok(new Result<Window> { Code = ErrorCode.SUCCESS, Data = window });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<Window> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result<Window>>> Delete(Guid id)
    {
        try
        {
            var window = await _windowService.DeleteWindowAsync(id);
            return Ok(new Result<Window> { Code = ErrorCode.SUCCESS, Data = window });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<Window> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<Window>>>> GetAll()
    {
        try
        {
            var windows = await _windowService.GetAllWindowsAsync();
            return Ok(new Result<List<Window>> { Code = ErrorCode.SUCCESS, Data = windows });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<List<Window>> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<Window>>> GetById(Guid id)
    {
        try
        {
            var window = await _windowService.GetWindowByIdAsync(id);
            return Ok(new Result<Window> { Code = ErrorCode.SUCCESS, Data = window });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<Window> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpGet("{canteen}")]
    public async Task<ActionResult<Result<List<Window>>>> GetByCanteen(string canteen)
    {
        try
        {
            var windows = await _windowService.GetWindowsByCanteenAsync(canteen);
            return Ok(new Result<List<Window>> { Code = ErrorCode.SUCCESS, Data = windows });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<List<Window>> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpPost("{id}")]
    [Authorize(Roles = "ADMIN,USER")]
    public async Task<ActionResult<Result<int>>> IncreaseFavoriteCount(Guid id)
    {
        try
        {
            var cnt = await _windowService.IncreaseFavoriteCountAsync(id);
            return Ok(new Result<int> { Code = ErrorCode.SUCCESS, Data = cnt });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<int> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result<Window>>> Update(
        Guid id,
        UpdateWindowParams updateWindowParams
    )
    {
        try
        {
            var window = await _windowService.UpdateWindowAsync(id, updateWindowParams);
            return Ok(new Result<Window> { Code = ErrorCode.SUCCESS, Data = window });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<Window> { Code = ex.Code, Error = ex.Message });
        }
    }
}
