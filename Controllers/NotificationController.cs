using FoodieLionApi.Models;
using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Models.Params;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodieLionApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result<Notification>>> Create(NotificationDto notificationDto)
    {
        try
        {
            var notification = await _notificationService.CreateNotificationAsync(notificationDto);
            return Ok(new Result<Notification> { Code = ErrorCode.SUCCESS, Data = notification });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<Notification> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result<Notification>>> Delete(Guid id)
    {
        try
        {
            var notification = await _notificationService.DeleteNotificationAsync(id);
            return Ok(new Result<Notification> { Code = ErrorCode.SUCCESS, Data = notification });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<Notification> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<Notification>>>> GetAll()
    {
        try
        {
            var notifications = await _notificationService.GetAllNotificationsAsync();
            return Ok(
                new Result<List<Notification>> { Code = ErrorCode.SUCCESS, Data = notifications }
            );
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(
                new Result<List<Notification>> { Code = ex.Code, Error = ex.Message }
            );
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<Notification>>> GetById(Guid id)
    {
        try
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            return Ok(new Result<Notification> { Code = ErrorCode.SUCCESS, Data = notification });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<Notification> { Code = ex.Code, Error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Result<Notification>>> Update(
        Guid id,
        UpdateNotificationParams updateNotificationParams
    )
    {
        try
        {
            var notification = await _notificationService.UpdateNotificationAsync(
                id,
                updateNotificationParams
            );
            return Ok(new Result<Notification> { Code = ErrorCode.SUCCESS, Data = notification });
        }
        catch (FoodieLionException ex)
        {
            return BadRequest(new Result<Notification> { Code = ex.Code, Error = ex.Message });
        }
    }
}
