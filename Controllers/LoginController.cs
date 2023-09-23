using FoodieLionApi.Models;
using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FoodieLionApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    public async Task<ActionResult<Result<string>>> Login([FromBody] LoginDTO loginDto)
    {
        try
        {
            var token = await _loginService.Login(loginDto.UserNameOrEmail, loginDto.Password);
            return new Result<string> { Code = ErrorCode.SUCCESS, Data = token };
        }
        catch (FoodieLionException e)
        {
            return BadRequest(new Result<string>() { Code = e.Code, Error = e.Message });
        }
    }
}
