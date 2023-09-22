using FoodieLionApi.Models;
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
}
