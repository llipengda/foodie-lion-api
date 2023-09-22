using FoodieLionApi.Models.Configurations;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Models.Params;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.Entities;

[EntityTypeConfiguration(typeof(UserConfiguration))]
public class User
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [DefaultValue(UserRole.USER)]
    public UserRole Role { get; set; } = UserRole.USER;

    public string? Avatar { get; set; }

    public string? Signature { get; set; }

    public void Update(UpdateUserParams updateParams)
    {
        if (!string.IsNullOrEmpty(updateParams.Name))
        {
            Name = updateParams.Name;
        }
        if (!string.IsNullOrEmpty(updateParams.Email))
        {
            Email = updateParams.Email;
        }
        if (updateParams.Role != null)
        {
            Role = updateParams.Role.Value;
        }
        if (!string.IsNullOrEmpty(updateParams.Avatar))
        {
            Avatar = updateParams.Avatar;
        }
        if (!string.IsNullOrEmpty(updateParams.Signature))
        {
            Signature = updateParams.Signature;
        }
    }
}
