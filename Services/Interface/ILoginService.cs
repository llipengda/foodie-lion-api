namespace FoodieLionApi.Services.Interface;

public interface ILoginService
{
    public Task<string> Login(string userNameOrEmail, string password);
}
