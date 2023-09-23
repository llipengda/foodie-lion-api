using AutoMapper;
using FoodieLionApi.Models;
using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Models.Params;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FoodieLionApi.Services;

public class WindowService : IWindowService
{
    private readonly FoodieLionDbContext _context;
    private readonly IMapper _mapper;

    public WindowService(FoodieLionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Window> CreateWindowAsync(WindowDto windowDto)
    {
        if (string.IsNullOrEmpty(windowDto.Name))
        {
            throw new FoodieLionException("Name is required", ErrorCode.INVALID_INPUT);
        }
        if (string.IsNullOrEmpty(windowDto.Canteen))
        {
            throw new FoodieLionException("Canteen is required", ErrorCode.INVALID_INPUT);
        }
        if (
            await _context.Windows.AnyAsync(
                w => w.Name == windowDto.Name && w.Canteen == windowDto.Canteen
            )
        )
        {
            throw new FoodieLionException("Window already exists", ErrorCode.WINDOW_ALREADY_EXISTS);
        }
        var window = _mapper.Map<Window>(windowDto);
        var added = await _context.AddAsync(window);
        await _context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Window> DeleteWindowAsync(Guid windowId)
    {
        var window =
            await _context.Windows.FindAsync(windowId)
            ?? throw new FoodieLionException("Window not found", ErrorCode.WINDOW_NOT_FOUND);
        _context.Windows.Remove(window);
        await _context.SaveChangesAsync();
        return window;
    }

    public async Task<List<Window>> GetAllWindowsAsync()
    {
        return await _context.Windows.ToListAsync();
    }

    public async Task<Window> GetWindowByIdAsync(Guid windowId)
    {
        return await _context.Windows.FindAsync(windowId)
            ?? throw new FoodieLionException("Window not found", ErrorCode.WINDOW_NOT_FOUND);
    }

    public async Task<List<Window>> GetWindowsByCanteenAsync(string canteen)
    {
        return await _context.Windows.Where(w => w.Canteen == canteen).ToListAsync();
    }

    public async Task<int> IncreaseFavoriteCountAsync(Guid windowId)
    {
        var window =
            await _context.Windows.FindAsync(windowId)
            ?? throw new FoodieLionException("Window not found", ErrorCode.WINDOW_NOT_FOUND);
        var cnt = ++window.FavoriteCount;
        await _context.SaveChangesAsync();
        return cnt;
    }

    public async Task<int> DecreaseFavoriteCountAsync(Guid windowId)
    {
        var window =
            await _context.Windows.FindAsync(windowId)
            ?? throw new FoodieLionException("Window not found", ErrorCode.WINDOW_NOT_FOUND);
        var cnt = --window.FavoriteCount;
        await _context.SaveChangesAsync();
        return cnt;
    }

    public async Task<Window> UpdateWindowAsync(
        Guid windowId,
        UpdateWindowParams updateWindowParams
    )
    {
        var window =
            await _context.Windows.FindAsync(windowId)
            ?? throw new FoodieLionException("Window not found", ErrorCode.WINDOW_NOT_FOUND);
        if (
            await _context.Windows.AnyAsync(
                w => w.Name == updateWindowParams.Name && w.Canteen == updateWindowParams.Canteen
            )
        )
        {
            throw new FoodieLionException("Window already exists", ErrorCode.WINDOW_ALREADY_EXISTS);
        }
        window.Update(updateWindowParams);
        await _context.SaveChangesAsync();
        return window;
    }
}
