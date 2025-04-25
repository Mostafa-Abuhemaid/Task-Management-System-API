using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entites;

namespace Web.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(AppUser user, UserManager<AppUser> userManager);

    }
}
