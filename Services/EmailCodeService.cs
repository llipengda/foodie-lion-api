using FoodieLionApi.Models;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace FoodieLionApi.Services;

public class EmailCodeService : IEmailCodeService
{
    private readonly FoodieLionDbContext _context;
    private readonly ILogger<EmailCodeService> _logger;
    private readonly IConfiguration _configuration;
    private readonly SmtpClient _smtpClient;

    public EmailCodeService(
        FoodieLionDbContext context,
        ILogger<EmailCodeService> logger,
        IConfiguration configuration
    )
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
        _smtpClient = new()
        {
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Host = _configuration.GetValue<string>("Smtp:Host"),
            Port = _configuration.GetValue<int>("Smtp:Port"),
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(
                _configuration.GetValue<string>("Smtp:Username"),
                _configuration.GetValue<string>("Smtp:Password")
            ),
        };

        _smtpClient.SendCompleted += (sender, e) =>
        {
            if (e.Error != null)
            {
                _logger.LogError(e.Error, "[EMAIL] Failed to send email");
            }
            else
            {
                _logger.LogInformation("[EMAIL] Email sent successfully");
            }
        };
    }

    public async Task<Guid> SendAsync(string email)
    {
        var code = new Random().Next(100000, 999999).ToString();
        var message = new MailMessage(
            _configuration.GetValue<string>("Smtp:UserName"),
            email,
            "Foodie Lion Email Verification",
            $"Your verification code is {code}"
        );
        await _smtpClient.SendMailAsync(message);
        var res = await _context.AddAsync(new EmailCode { Email = email, Code = code });
        await _context.SaveChangesAsync();
        return res.Entity.Id;
    }

    public async Task<bool> VerifyAsync(string email, string code)
    {
        var res = await _context.EmailCodes.FirstOrDefaultAsync(
            ec => ec.Email == email && ec.Code == code && ec.ExpiredAt > DateTime.UtcNow
        );
        return res is not null;
    }
}
