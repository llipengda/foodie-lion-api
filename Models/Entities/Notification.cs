using FoodieLionApi.Models.Configurations;
using FoodieLionApi.Models.Params;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FoodieLionApi.Models.Entities;

[EntityTypeConfiguration(typeof(NotificationConfiguration))]
public class Notification
{
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public bool? IsVisiable { get; set; } = true;

    public void Update(UpdateNotificationParams updateParams)
    {
        if (!string.IsNullOrEmpty(updateParams.Title))
        {
            Title = updateParams.Title;
        }

        if (!string.IsNullOrEmpty(updateParams.Content))
        {
            Content = updateParams.Content;
        }

        UpdatedAt = DateTime.Now;

        if (updateParams.IsVisiable != null)
        {
            IsVisiable = updateParams.IsVisiable;
        }
    }
}
