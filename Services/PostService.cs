using AutoMapper;
using FoodieLionApi.Models;
using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FoodieLionApi.Services;

public class PostService : IPostService
{
    private readonly FoodieLionDbContext _context;

    private readonly IMapper _mapper;

    public PostService(FoodieLionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Post> CreatePostAsync(PostDto postInDto)
    {
        if (await _context.Users.FirstOrDefaultAsync(u => u.Name == postInDto.UserName) == null)
        {
            throw new FoodieLionException("User not found", ErrorCode.USER_NOT_FOUND);
        }
        var added = await _context.Posts.AddAsync(_mapper.Map<Post>(postInDto));
        await _context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Post> DeletePostAsync(int postId)
    {
        var post =
            await _context.Posts.FindAsync(postId)
            ?? throw new FoodieLionException("Post not found", ErrorCode.POST_NOT_FOUND);
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<List<Post>> GetAllPostsAsync()
    {
        return await _context.Posts.OrderByDescending(p => p.CreatedAt).ToListAsync();
    }
}
