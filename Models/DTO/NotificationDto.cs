using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.DTO;

public class NotificationDto
{
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;

    public bool? IsVisiable { get; set; } = true;
}
