using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.UserDTO;
using Web.Application.Response;

namespace Web.Application.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<UserDto>> GetUserDetailsAsync(string userId);
        Task<BaseResponse<List<UserDto>>> GetAllUsersAsync();
        Task<BaseResponse<bool>> EditUserAsync( [FromBody] UserDto model);

        Task<BaseResponse<bool>> LockUserByEmailAsync(string email);
        Task<BaseResponse<bool>> UnlockUserByEmailAsync(string email);
        Task<BaseResponse<bool>> DeleteUserByEmailAsync(string email);
    }
}
