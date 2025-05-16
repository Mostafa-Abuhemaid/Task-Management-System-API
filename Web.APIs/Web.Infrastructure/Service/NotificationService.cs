using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Interfaces;
using Web.Domain.Hubs;

namespace Web.Infrastructure.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendTaskReminder(string userId, string message)
        {
            await _hubContext.Clients.User(userId).SendAsync("ReceiveReminder", message);
        }
    }
}
