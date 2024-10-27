using Microsoft.AspNetCore.SignalR;

namespace NotificationApp.Hubs
{
    public class NotificationHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;

            Console.WriteLine(Context.UserIdentifier);
            Console.WriteLine(Context.ConnectionId);
            Console.WriteLine(Context.User);

            return base.OnConnectedAsync();
        }
    }
}
