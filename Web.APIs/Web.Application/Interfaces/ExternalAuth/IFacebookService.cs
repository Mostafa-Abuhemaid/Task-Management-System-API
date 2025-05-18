using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.AccountDTO;
using Web.Application.Response;

namespace Web.Application.Interfaces.ExternalAuth
{
    public interface IFacebookService
    {
        Task<BaseResponse<TokenDTO>> FacebookSignInAsync(string TokenId);
    }
}
