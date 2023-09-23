using FoodieLionApi.Models;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FoodieLionApi.Services;

public class UserLikeService : IUserLikeService
{
    private readonly FoodieLionDbContext _context;

    public UserLikeService(FoodieLionDbContext context)
    {
        _context = context;
    }

    public async Task<UserLike> CreateAsync(string userName, string name)
    {
        if (await _context.UserLikes.AnyAsync(u => u.UserName == userName && u.LikedName == name))
        {
            throw new FoodieLionException(
                "User-like already exists",
                ErrorCode.USER_LIKE_ALREADY_EXISTS
            );
        }
        var added = await _context.UserLikes.AddAsync(
            new() { UserName = userName, LikedName = name }
        );
        await _context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<UserLike> DeleteAsync(string userName, string name)
    {
        var toDel =
            await _context.UserLikes.FirstOrDefaultAsync(
                u => u.UserName == userName && u.LikedName == name
            )
            ?? throw new FoodieLionException("User-like not exist", ErrorCode.USER_LIKE_NOT_FOUND);
        _context.UserLikes.Remove(toDel);
        await _context.SaveChangesAsync();
        return toDel;
    }

    public async Task<bool> GetAsync(string userName, string name)
    {
        return await _context.UserLikes.FirstOrDefaultAsync(
            u => u.UserName == userName && u.LikedName == name
        )
            is not null;
    }
}
