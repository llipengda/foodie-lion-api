using FoodieLionApi.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.Entities;

[EntityTypeConfiguration(typeof(UserLikeConfiguration))]
public class UserLike
{
    public Guid Id { get; set; }

    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string LikedName { get; set; } = null!;
}
