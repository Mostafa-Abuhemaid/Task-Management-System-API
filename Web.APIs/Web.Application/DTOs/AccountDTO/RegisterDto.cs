using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.DTOs.AccountDTO
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "The email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Password is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "The ConfirmedPassword is Required")]
        public string ConfirmPassword { get; set; }

    }
}
