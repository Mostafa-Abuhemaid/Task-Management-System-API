using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Enums;

namespace Web.Application.DTOs.AccountDTO
{
    public class TokenDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
      

        public string Token { get; set; }
    }
}
