namespace FoodieLionApi.Services.Interface;

public interface IEmailCodeService
{
    Task<Guid> SendAsync(string email);

    Task<bool> VerifyAsync(string email, string code);
}
