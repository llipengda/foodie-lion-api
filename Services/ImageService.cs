using FoodieLionApi.Models.Enums;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;

namespace FoodieLionApi.Services;

public class ImageService : IImageService
{
    private readonly IConfiguration _configuration;

    public ImageService(IConfiguration configuration)
    {
        _configuration = configuration;
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
}
