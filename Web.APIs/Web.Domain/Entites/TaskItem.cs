using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Enums;

namespace Web.Domain.Entites
{
    public class TaskItem:BaseClass<int>
    {
      public string Title {  get; set; }
      public string Description { get; set; }
      public DateTime DueDate { get; set; }
       public Status Status { get; set; } = Status.Pending;
      public int CategoryId {  get; set; }
      public Category Category { get; set; }
      public string UserId { get; set; }
      public AppUser User { get; set; }

    }
}
