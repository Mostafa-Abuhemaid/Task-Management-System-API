using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Interfaces;
using Web.Domain.Hubs;
using Web.Infrastructure.Data;

namespace Web.Infrastructure.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly AppDbContext _DbContext;
        public NotificationService(IHubContext<NotificationHub> hubContext, AppDbContext dbContext)
        {
            _hubContext = hubContext;
            _DbContext = dbContext;
        }
        public async Task SendTaskReminder(int TaskId)
        {
            var task = await _DbContext.Tasks
             .Include(t => t.User)
             .FirstOrDefaultAsync(t => t.Id == TaskId);
            {
                await _hubContext.Clients.User(task.UserId).SendAsync("ReceiveTaskReminder", new
                {
                    task.Id,
                    task.Title,
                    task.Description,
                    DueDate = task.DueDate.ToString("f"), 
                    Message = $"Reminder: '{task.Title}' is due now!"
                });
            }
           
        }
    }
}
