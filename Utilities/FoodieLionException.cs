using FoodieLionApi.Models.Enums;

namespace FoodieLionApi.Utilities;

public class FoodieLionException : Exception
{
    public ErrorCode Code { get; set; }

    public FoodieLionException(string message, ErrorCode code)
        : base(message)
    {
        Code = code;
    }
}
