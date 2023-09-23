using FoodieLionApi.Models.Entities;

namespace FoodieLionApi.Services.Interface;

public interface IUserLikeService
{
    Task<bool> GetAsync(string userName, string name);

    Task<UserLike> CreateAsync(string userName, string name);

    Task<UserLike> DeleteAsync(string userName, string name);
}
