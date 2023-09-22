using FoodieLionApi.Models;
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

    [HttpPost("{username}/{password}")]
    public async Task<ActionResult<Result<string>>> Login(string username, string password)
    {
        try
        {
            var token = await _loginService.Login(username, password);
            return new Result<string> { Code = ErrorCode.SUCCESS, Data = token };
        }
        catch (FoodieLionException e)
        {
            ActionResult<Result<string>> res = e.Code switch
            {
                ErrorCode.USER_NOT_FOUND
                    => BadRequest(
                        new Result<string> { Code = ErrorCode.USER_NOT_FOUND, Error = e.Message }
                    ),
                ErrorCode.INVALID_PASSWORD
                    => Unauthorized(
                        new Result<string> { Code = ErrorCode.INVALID_PASSWORD, Error = e.Message }
                    ),
                _
                    => BadRequest(
                        new Result<string> { Code = ErrorCode.BAD_REQUEST, Error = e.Message }
                    )
            };
            return res;
        }
    }
}
