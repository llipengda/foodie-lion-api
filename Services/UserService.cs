using AutoMapper;
using FoodieLionApi.Models;
using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Models.Params;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FoodieLionApi.Services;

public class UserService : IUserService
{
    private readonly FoodieLionDbContext _context;

    private readonly IMapper _mapper;

    public UserService(FoodieLionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserOutDto> CreateUserAsync(User user)
    {
        if (string.IsNullOrEmpty(user.Name))
        {
            throw new FoodieLionException("Name is required", ErrorCode.INVALID_INPUT);
        }
        if (string.IsNullOrEmpty(user.Email))
        {
            throw new FoodieLionException("Email is required", ErrorCode.INVALID_INPUT);
        }
        if (await _context.Users.AnyAsync(u => u.Email == user.Email || u.Name == user.Name))
        {
            throw new FoodieLionException("User already exists", ErrorCode.USER_ALREADY_EXISTS);
        }
        user.Id = Guid.NewGuid();
        var bytes = Encoding.UTF8.GetBytes(user.Password + user.Id);
        var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(bytes);
        user.Password = Convert.ToBase64String(hash);
        var addedUser = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return _mapper.Map<UserOutDto>(addedUser.Entity);
    }

    public async Task<UserOutDto> DeleteUserAsync(Guid id)
    {
        var user =
            await _context.Users.FindAsync(id)
            ?? throw new FoodieLionException("User not found", ErrorCode.USER_NOT_FOUND);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return _mapper.Map<UserOutDto>(user);
    }

    public async Task<List<UserOutDto>> GetAllUsersAsync()
    {
        return await _context.Users.Select(u => _mapper.Map<UserOutDto>(u)).ToListAsync();
    }

    public async Task<UserOutDto> GetUserByIdAsync(Guid id)
    {
        return _mapper.Map<UserOutDto>(
            await _context.Users.FindAsync(id)
                ?? throw new FoodieLionException("User not found", ErrorCode.USER_NOT_FOUND)
        );
    }

    public async Task<List<UserOutDto>> GetUsersByNameAsync(string name)
    {
        return await _context.Users
            .Where(u => u.Name.Contains(name))
            .Select(u => _mapper.Map<UserOutDto>(u))
            .ToListAsync();
    }

    public async Task<UserOutDto> GetUserByNameOrEmailAsync(string nameOrEmail)
    {
        var user =
            await _context.Users.FirstOrDefaultAsync(
                u => u.Name == nameOrEmail || u.Email == nameOrEmail
            ) ?? throw new FoodieLionException("User not found", ErrorCode.USER_NOT_FOUND);
        return _mapper.Map<UserOutDto>(user);
    }

    public async Task<UserOutDto> GetUserByEmailAsync(string email)
    {
        return _mapper.Map<UserOutDto>(
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new FoodieLionException("User not found", ErrorCode.USER_NOT_FOUND)
        );
    }

    public async Task<UserOutDto> UpdateUserAsync(Guid id, UpdateUserParams updateParams)
    {
        var user =
            await _context.Users.FindAsync(id)
            ?? throw new FoodieLionException("user not found", ErrorCode.USER_NOT_FOUND);
        if (
            await _context.Users.AnyAsync(
                u => u.Email == updateParams.Email || u.Name == updateParams.Name
            )
        )
        {
            throw new FoodieLionException("User already exists", ErrorCode.USER_ALREADY_EXISTS);
        }
        user.Update(updateParams);
        if (!string.IsNullOrEmpty(updateParams.Password))
        {
            var bytes = Encoding.UTF8.GetBytes(updateParams.Password + user.Id);
            var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(bytes);
            user.Password = Convert.ToBase64String(hash);
        }
        await _context.SaveChangesAsync();
        return _mapper.Map<UserOutDto>(user);
    }

    public async Task<bool> VerifyName(string name)
    {
        return !await _context.Users.AnyAsync(u => u.Name == name);
    }

    public async Task<bool> VerifyEmail(string email)
    {
        return !await _context.Users.AnyAsync(u => u.Email == email);
    }
}
