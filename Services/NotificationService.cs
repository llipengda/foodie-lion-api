using AutoMapper;
using FoodieLionApi.Models;
using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Models.Params;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FoodieLionApi.Services;

public class NotificationService : INotificationService
{
    private readonly FoodieLionDbContext _context;

    private readonly IMapper _mapper;

    public NotificationService(FoodieLionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Notification> CreateNotificationAsync(NotificationDto notificationDto)
    {
        if (string.IsNullOrEmpty(notificationDto.Title))
        {
            throw new FoodieLionException("Title is required", ErrorCode.INVALID_INPUT);
        }
        if (string.IsNullOrEmpty(notificationDto.Content))
        {
            throw new FoodieLionException("Content is required", ErrorCode.INVALID_INPUT);
        }
        var added = await _context.Notifications.AddAsync(
            _mapper.Map<Notification>(notificationDto)
        );
        await _context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Notification> DeleteNotificationAsync(Guid notificationId)
    {
        var notification =
            await _context.Notifications.FindAsync(notificationId)
            ?? throw new FoodieLionException(
                "Notification not found",
                ErrorCode.NOTIFICATION_NOT_FOUND
            );
        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
        return notification;
    }

    public async Task<List<Notification>> GetAllNotificationsAsync()
    {
        return await _context.Notifications.OrderByDescending(n => n.UpdatedAt).ToListAsync();
    }

    public async Task<Notification> GetNotificationByIdAsync(Guid notificationId)
    {
        return await _context.Notifications.FirstOrDefaultAsync(
                notification => notification.Id == notificationId
            )
            ?? throw new FoodieLionException(
                "Notification not found",
                ErrorCode.NOTIFICATION_NOT_FOUND
            );
    }

    public async Task<Notification> UpdateNotificationAsync(
        Guid notificationId,
        UpdateNotificationParams updateNotificationParams
    )
    {
        var notification =
            await _context.Notifications.FindAsync(notificationId)
            ?? throw new FoodieLionException(
                "Notification not found",
                ErrorCode.NOTIFICATION_NOT_FOUND
            );
        notification.Update(updateNotificationParams);
        await _context.SaveChangesAsync();
        return notification;
    }
}
