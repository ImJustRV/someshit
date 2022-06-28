using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace MinaCoolProgekt.Hubs;

public class ProjectChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}