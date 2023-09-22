using FoodieLionApi.Models;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FoodieLionApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class EmailCodeController : ControllerBase
{
    private readonly IEmailCodeService _emailCodeService;

    public EmailCodeController(IEmailCodeService emailCodeService)
    {
        _emailCodeService = emailCodeService;
    }

    [HttpPost("{email}")]
    public async Task<ActionResult<Result<Guid>>> Send(string email)
    {
        var code = await _emailCodeService.SendAsync(email);
        var res = new Result<Guid> { Code = ErrorCode.SUCCESS, Data = code };
        return Ok(res);
    }

    [HttpPost("{email}/{code}")]
    public async Task<ActionResult<Result<bool>>> Verify(string email, string code)
    {
        var res = new Result<bool>
        {
            Code = ErrorCode.SUCCESS,
            Data = await _emailCodeService.VerifyAsync(email, code)
        };
        return Ok(res);
    }
}
