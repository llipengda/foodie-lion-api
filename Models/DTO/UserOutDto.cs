using FoodieLionApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.DTO;

public class UserOutDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public UserRole Role { get; set; }

    public string? Avatar { get; set; }

    public string? Signature { get; set; }
}
