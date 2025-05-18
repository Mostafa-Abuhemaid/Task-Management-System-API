using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.UserDTO;
using Web.Application.Interfaces;
using Web.Application.Response;
using Web.Domain.Entites;

namespace Web.Infrastructure.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<BaseResponse<bool>> DeleteUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return new BaseResponse<bool>(false, $"No User with this email : {email}");

            var result = await _userManager.DeleteAsync(user);
            return new BaseResponse<bool>(true, $"User {email} deleted successfully");
        }

        public async Task<BaseResponse<bool>> EditUserAsync([FromBody] UserDto model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) new BaseResponse<bool>(false, $"No User with this email : {model.Id}");


            user.UserName = model.UserName;
            user.Email = model.Email;
           

            var result = await _userManager.UpdateAsync(user);
            return new BaseResponse<bool>(true, $"User {model.UserName} Updated successfully");
        }

        public async Task<BaseResponse<List<UserDto>>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users);
           

            return new BaseResponse<List < UserDto >> (true, $"Reached Users successfully ", userDtos);
        }

        public async Task<BaseResponse<UserDto>> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new BaseResponse<UserDto>(false, $"No User with this Id : {userId}");
            var userDTO = _mapper.Map<UserDto>(user);

            return new BaseResponse<UserDto>(true, $"Reached User with Id : {userId}",userDTO);
        }

        public async Task<BaseResponse<bool>> LockUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) new BaseResponse<bool>(false, $"No User with this email : {email}");

            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            return new BaseResponse<bool>(true, $"User {user.UserName} locked successfully");
        }

        public async Task<BaseResponse<bool>> UnlockUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)return new BaseResponse<bool>(false, $"No User with this email : {email}");

            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
            return new BaseResponse<bool>(true, $"User {user.UserName} unlocked successfully");
        }
    }
}
