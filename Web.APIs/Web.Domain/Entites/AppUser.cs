using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Enums;

namespace Web.Domain.Entites
{
    public class AppUser : IdentityUser
    {
        public ICollection<TaskItem> Tasks { get; set; } =new HashSet<TaskItem>();
        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        public ICollection<Notification> Notifications { get; set; }= new HashSet<Notification>();
    }
}
