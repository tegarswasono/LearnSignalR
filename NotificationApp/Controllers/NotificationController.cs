using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotificationApp.Hubs;

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
    }
}
