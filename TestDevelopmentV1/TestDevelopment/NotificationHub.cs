using Microsoft.AspNetCore.SignalR;
using TestDevelopment.DataService;
using TestDevelopment.Models;

namespace TestDevelopment
{
    public class NotificationHub : Hub
    {

        private readonly SharedDb _shared;

        public NotificationHub(SharedDb shared)=> _shared = shared;
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }

        public async Task JoinChat(UserConnectionModel model)
        {
            await Clients.All.SendAsync("ReceiveMessage", "Admin", $"{model.UserName} has Joined the chat");
        }

        public async Task JoinSpecificChatRoom(UserConnectionModel model)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, model.ChatRoom);
            _shared.connections[Context.ConnectionId] = model;
            await Clients.Groups(model.ChatRoom).SendAsync("ConnectRoom", "Admin", $"User {model.UserName} has Joined Chat {model.ChatRoom}");
        }

        public async Task SendMessage(string message)
        {
            try
            {
                if (_shared.connections.TryGetValue(Context.ConnectionId, out UserConnectionModel model))
                {
                    await Clients.Groups(model.ChatRoom).SendAsync("RecieveSpecificMessage", model.UserName, message);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
