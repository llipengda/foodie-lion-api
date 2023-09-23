using FoodieLionApi.Services;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace FoodieLionApi.Utilities;

public static class Extension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmailCodeService, EmailCodeService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IWindowService, WindowService>();
        services.AddScoped<IDishService, DishService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IUserLikeService, UserLikeService>();
    }

    public static void AddFilters(this MvcOptions option)
    {
        option.Filters.Add<LoggingFilter>();
        option.Filters.Add<ExceptionFilter>();
    }

    public static void SetupSwagger(this SwaggerGenOptions option)
    {
        option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "foodie-lion-api.xml"));
        option.DocumentFilter<EnumDoucumentFilter>();
        option.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            }
        );
        option.AddSecurityDefinition(
            "Bearer",
            new OpenApiSecurityScheme
            {
                Description = "Value: Bearer {token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            }
        );
    }

    public static void AddJwtAuthentication(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])
                    )
                };
                option.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (
                            !string.IsNullOrEmpty(accessToken)
                            && (path.StartsWithSegments("/Hub/PostHub"))
                        )
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
    }
}
