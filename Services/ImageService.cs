using FoodieLionApi.Models;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FoodieLionApi.Services;

public class ImageService : IImageService
{
    private readonly IConfiguration _configuration;

    private readonly FoodieLionDbContext _context;

    public ImageService(IConfiguration configuration, FoodieLionDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    private readonly string[] permittedExtensions = { ".jpg", ".png", ".jpeg", ".bmp" };

    public async Task<string> Upload(IFormFile file)
    {
        if (file is null)
        {
            throw new FoodieLionException("File is null", ErrorCode.FILE_IS_NULL);
        }
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
        {
            throw new FoodieLionException("Invalid file type", ErrorCode.INVALID_FILE_TYPE);
        }
        var path = $"static/images/{Path.GetRandomFileName()}{ext}";
        var filePath = $"{Directory.GetCurrentDirectory()}/wwwroot/{path}";
        var urlPath = $"{_configuration["Host"]}/{path}";
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/wwwroot/static/images");
        }
        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        return urlPath;
    }

    public async Task<List<HomeImage>> GetHomeImages()
    {
        return await _context.HomeImages.ToListAsync();
    }

    public async Task<HomeImage> AddHomeImage(string url)
    {
        var added = await _context.HomeImages.AddAsync(new HomeImage { Url = url });
        await _context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<HomeImage> DeleteHomeImage(Guid id)
    {
        var toDel =
            await _context.HomeImages.FindAsync(id)
            ?? throw new FoodieLionException("Image not found", ErrorCode.IMAGE_NOT_FOUND);
        _context.HomeImages.Remove(toDel);
        await _context.SaveChangesAsync();
        return toDel;
    }
}
