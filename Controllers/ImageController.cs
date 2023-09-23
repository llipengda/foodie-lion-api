using FoodieLionApi.Models;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN,USER")]
    public async Task<ActionResult<Result<string>>> Upload([Required] IFormFile image)
    {
        try
        {
            var res = await _imageService.Upload(image);
            return Ok(new Result<string> { Code = ErrorCode.SUCCESS, Data = res });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<string> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<HomeImage>>>> GetHomeImages()
    {
        var res = await _imageService.GetHomeImages();
        return Ok(new Result<List<HomeImage>> { Code = ErrorCode.SUCCESS, Data = res });
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result<HomeImage>>> AddHomeImage([Required] string url)
    {
        var res = await _imageService.AddHomeImage(url);
        return Ok(new Result<HomeImage> { Code = ErrorCode.SUCCESS, Data = res });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result<HomeImage>>> DeleteHomeImage(Guid id)
    {
        try
        {
            var res = await _imageService.DeleteHomeImage(id);
            return Ok(new Result<HomeImage> { Code = ErrorCode.SUCCESS, Data = res });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<HomeImage> { Code = ex.Code, Error = ex.Message });
        }
    }
}
