using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Enums;

namespace Web.Application.DTOs.TaskDTO
{
    public class GetTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public string Description { get; set; }
       
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
