using FoodieLionApi.Models.DTO;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FoodieLionApi.Hubs;

public class PostHub : Hub
{
    private readonly ILogger<PostHub> _logger;

    private readonly IPostService _postService;

    public PostHub(ILogger<PostHub> logger, IPostService postService)
    {
        _logger = logger;
        _postService = postService;
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation("[SignalR] PostHub is Connected");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("[SignalR] PostHub is Disonnected");
        return base.OnDisconnectedAsync(exception);
    }

    [Authorize(Roles = "ADMIN,USER")]
    public async Task SendPost(PostDto postDto)
    {
        try
        {
            var post = await _postService.CreatePostAsync(postDto);
            await Clients.All.SendAsync("ReceivePost", post);
        }
        catch (FoodieLionException e)
        {
            await Clients.Caller.SendAsync("ReceiveError", e.Code, e.Message);
        }
    }
}
