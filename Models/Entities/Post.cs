using FoodieLionApi.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.Entities;

[EntityTypeConfiguration(typeof(PostConfiguration))]
public class Post
{
    public Guid Id { get; set; }

    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string? Avatar { get; set; }
}
