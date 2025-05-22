using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.AccountDTO;
using Web.Application.Response;

namespace Web.Application.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<TokenDTO>> RegisterAsync(RegisterDto loginDto);
        Task<BaseResponse<TokenDTO>> LoginAsync(LoginDTO loginDto);
        Task<BaseResponse<string>> ForgotPasswordAsync(ForgetPasswordDto request);
        Task<BaseResponse<bool>> VerifyOTPAsync(VerfiyCodeDto verify);
        Task<BaseResponse<bool>> ResetPasswordAsync(ResetPasswordDto resetPassword);
        Task<BaseResponse<bool>> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto);
    }
}
