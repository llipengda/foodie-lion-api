using FoodieLionApi.Models;
using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodieLionApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN,USER")]
    public async Task<ActionResult<Result<Post>>> Create(PostDto postDto)
    {
        try
        {
            var post = await _postService.CreatePostAsync(postDto);
            return Ok(new Result<Post>(post));
        }
        catch (FoodieLionException e)
        {
            return BadRequest(new Result<Post>() { Error = e.Message, Code = e.Code });
        }
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<Post>>>> GetAll()
    {
        var posts = await _postService.GetAllPostsAsync();
        return Ok(new Result<List<Post>>(posts));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result<Post>>> Delete(int id)
    {
        try
        {
            var post = await _postService.DeletePostAsync(id);
            return Ok(new Result<Post>(post));
        }
        catch (FoodieLionException e)
        {
            return BadRequest(new Result<Post>() { Error = e.Message, Code = e.Code });
        }
    }
}
