namespace FoodieLionApi.Services.Interface;

public interface IImageService
{
    Task<string> Upload(IFormFile file);
}
