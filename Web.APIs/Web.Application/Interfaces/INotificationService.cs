using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendTaskReminder(string userId, string message);
    }
}
