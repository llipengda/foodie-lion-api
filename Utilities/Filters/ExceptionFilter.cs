using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FoodieLionApi.Models;
using FoodieLionApi.Models.Enums;

namespace FoodieLionApi.Utilities.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var method = context.HttpContext.Request.Method;
        var path = context.HttpContext.Request.Path;
        var query = context.HttpContext.Request.QueryString;
        _logger.LogError(
            context.Exception,
            "[REQUEST]  {method} {path}{query}",
            method,
            path,
            query
        );
        var result = new Result<object>
        {
            Code = ErrorCode.INTERNAL_SERVER_ERROR,
            Error = context.Exception.Message
        };
        context.Result = new ObjectResult(result)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
