using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.DTO;

public class LoginDTO
{
    [Required]
    public string UserNameOrEmail { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
