using FoodieLionApi.Models.Entities;

namespace FoodieLionApi.Services.Interface;

public interface IImageService
{
    Task<string> Upload(IFormFile file);

    Task<List<HomeImage>> GetHomeImages();
    
    Task<HomeImage> AddHomeImage(string url);

    Task<HomeImage> DeleteHomeImage(Guid id);
}
