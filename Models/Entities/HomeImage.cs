using FoodieLionApi.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.Entities;

[EntityTypeConfiguration(typeof(HomeImageConfiguration))]
public class HomeImage
{
    public Guid Id { get; set; }

    [Required]
    public string Url { get; set; } = null!;
}
