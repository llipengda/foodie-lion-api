using Microsoft.EntityFrameworkCore;
using FoodieLionApi.Models;
using FoodieLionApi.Utilities;
using FoodieLionApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddLog4Net();

builder.Services.AddDbContext<FoodieLionDbContext>(option =>
{
    var connectionString = builder.Configuration["ConnectionStrings:MySQLConnection"];
    var serverVersion = ServerVersion.AutoDetect(connectionString);
    option.UseMySql(connectionString, serverVersion);
});

builder.Services.AddControllers(option => option.AddFilters());

builder.Services.AddServices();

builder.Services.AddSignalR();

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(
        policy =>
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed(hostName => true)
    );
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SetupSwagger();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseCors();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<PostHub>("/hub/postHub");
});

app.Run();
