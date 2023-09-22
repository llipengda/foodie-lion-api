using FoodieLionApi.Models;
using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Params;

namespace FoodieLionApi.Services.Interface;

public interface IUserService
{
    Task<List<UserOutDto>> GetAllUsersAsync();

    Task<UserOutDto> GetUserByIdAsync(Guid id);

    Task<List<UserOutDto>> GetUserByNameAsync(string name);

    Task<UserOutDto> GetUserByEmailAsync(string email);

    Task<UserOutDto> CreateUserAsync(User user);

    Task<UserOutDto> UpdateUserAsync(Guid id, UpdateUserParams updateDto);

    Task<UserOutDto> DeleteUserAsync(Guid id);

    Task<bool> VerifyName(string name);

    Task<bool> VerifyEmail(string email);
}
