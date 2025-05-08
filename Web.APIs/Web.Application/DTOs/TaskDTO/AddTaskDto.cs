using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entites;
using Web.Domain.Enums;

namespace Web.Application.DTOs.TaskDTO
{
    public class AddTaskDto
    {
        [Required(ErrorMessage = "The Title is Required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The Description is Required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The DueDate is Required")]
        public DateTime DueDate { get; set; }
       
        public int CategoryId { get; set; }
    }
}
