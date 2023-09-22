using FoodieLionApi.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.Entities;

[EntityTypeConfiguration(typeof(EmailCodeConfiguration))]
public class EmailCode
{
    public Guid Id { get; set; }

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Code { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime ExpiredAt { get; set; } = DateTime.Now.AddMinutes(15);
}
