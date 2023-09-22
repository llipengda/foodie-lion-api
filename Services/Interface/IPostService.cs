using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;

namespace FoodieLionApi.Services.Interface;

public interface IPostService
{
    Task<Post> CreatePostAsync(PostDto postInDto);

    Task<List<Post>> GetAllPostsAsync();

    Task<Post> DeletePostAsync(int postId);
}
