using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotificationApp.Hubs;
using System.Collections.Concurrent;

namespace NotificationApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {

        private readonly IHubContext<NotificationHub> hubContext;
        public NotificationController(IHubContext<NotificationHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendNotification([FromBody] string message)
        {
            await hubContext.Clients.All.SendAsync("ReceiveNotification", message);
            return Ok(new { Message = "Notification Sent" });
        }

        private async Task SendNotificationToUsers(List<string> userIds, string message)
        {
            foreach (var userId in userIds)
            {
                if (ConnectedUsers.TryGetValue(userId, out var connectionId))
                {
                    // Kirim pesan ke pengguna tertentu berdasarkan Connection ID
                    await hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
                }
            }
        }

        private static readonly ConcurrentDictionary<string, string> ConnectedUsers = new ConcurrentDictionary<string, string>();
    }
}
