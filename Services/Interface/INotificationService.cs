using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Params;

namespace FoodieLionApi.Services.Interface;

public interface INotificationService
{
    Task<Notification> CreateNotificationAsync(NotificationDto notificationDto);

    Task<List<Notification>> GetAllNotificationsAsync();

    Task<Notification> GetNotificationByIdAsync(Guid notificationId);

    Task<Notification> UpdateNotificationAsync(Guid notificationId, UpdateNotificationParams updateNotificationParams);

    Task<Notification> DeleteNotificationAsync(Guid notificationId);
}
