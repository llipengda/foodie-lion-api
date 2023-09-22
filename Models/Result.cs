using FoodieLionApi.Models.Enums;

namespace FoodieLionApi.Models;

public class Result<T>
{
    /// <summary>
    /// A code representing the result of the operation
    /// </summary>
    public ErrorCode Code { get; set; } = ErrorCode.SUCCESS;

    /// <summary>
    /// The data of the request result
    /// </summary>
    public T? Data { get; set; } = default;

    /// <summary>
    /// The error message of the request result if any error happend
    /// </summary>
    public string? Error { get; set; } = null;

    public Result(T data)
    {
        Data = data;
    }

    public Result() { }
}
