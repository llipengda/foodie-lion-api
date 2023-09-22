using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.DTO;

public class UserDto
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    public string? Avatar { get; set; }

    public string? Signature { get; set; }
}
