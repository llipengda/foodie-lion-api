using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.DTO;

public class PostDto
{
    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;

    public string? Avatar { get; set; }
}
