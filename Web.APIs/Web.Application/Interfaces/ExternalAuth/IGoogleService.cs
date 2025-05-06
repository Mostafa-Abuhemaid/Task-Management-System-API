using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.AccountDTO;
using Web.Application.Response;
using Web.Domain.Entites;

namespace Web.Application.Interfaces.ExternalAuthService
{
    public interface IGoogleService
    {
        Task<BaseResponse<TokenDTO>> GoogleSignInAsync(string TokenId);
    }
}
