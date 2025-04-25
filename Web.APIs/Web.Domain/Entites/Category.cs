using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.Entites
{
    public class Category:BaseClass<int>
    {
        public string Name { get; set; }

        public ICollection<TaskItem> Tasks { get; set; }= new HashSet<TaskItem>();
    }
}
