using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Enums;

namespace FoodieLionApi.Models.Params;

public class UpdateUserParams
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public UserRole? Role { get; set; }

    public string? Avatar { get; set; }

    public string? Signature { get; set; }
}
