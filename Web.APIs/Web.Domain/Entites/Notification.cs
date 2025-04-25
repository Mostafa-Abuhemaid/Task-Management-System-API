using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.Entites
{
    public class Notification:BaseClass<int>
    {
        public string Message { get; set; }
        public DateTime dateTime { get; set; }= DateTime.Now;
        public int TaskId { get; set; }
        public TaskItem Task { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
