using FoodieLionApi.Models;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodieLionApi.Controllers;

[ApiController]
[Authorize(Roles = "ADMIN,USER")]
[Route("api/[controller]/[action]")]
public class UserLikeController : ControllerBase
{
    private readonly IUserLikeService _userLikeService;

    public UserLikeController(IUserLikeService userLikeService)
    {
        _userLikeService = userLikeService;
    }

    [HttpGet("{userName}/{name}")]
    public async Task<ActionResult<Result<bool>>> Get(string userName, string name)
    {
        try
        {
            var res = await _userLikeService.GetAsync(userName, name);
            return Ok(new Result<bool>(res));
        }
        catch (FoodieLionException e)
        {
            return BadRequest(new Result<bool>() { Code = e.Code, Error = e.Message });
        }
    }

    [HttpPost("{userName}/{name}")]
    public async Task<ActionResult<Result<UserLike>>> Create(string userName, string name)
    {
        try
        {
            var res = await _userLikeService.CreateAsync(userName, name);
            return Ok(new Result<UserLike>(res));
        }
        catch (FoodieLionException e)
        {
            return BadRequest(new Result<UserLike>() { Code = e.Code, Error = e.Message });
        }
    }

    [HttpDelete("{userName}/{name}")]
    public async Task<ActionResult<Result<UserLike>>> Delete(string userName, string name)
    {
        try
        {
            var res = await _userLikeService.DeleteAsync(userName, name);
            return Ok(new Result<UserLike>(res));
        }
        catch (FoodieLionException e)
        {
            return BadRequest(new Result<UserLike>() { Code = e.Code, Error = e.Message });
        }
    }
}
