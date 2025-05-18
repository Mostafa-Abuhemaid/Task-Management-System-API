using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Application.DTOs.UserDTO;
using Web.Application.Interfaces;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            
          var user =await _userService.GetUserDetailsAsync(userId);
            return user.Success ? Ok(user) : BadRequest();
        }


        [HttpPut("EditUser")]
        public async Task<IActionResult> EditUser([FromBody] UserDto model)
        {
            var user = await _userService.EditUserAsync(model);
            return user.Success ? Ok(user) : BadRequest();
        }
      //  [Authorize(Roles = "Admin")]
        [HttpPost("lock")]
        public async Task<IActionResult> LockUser(string email)
        {
            var user = await _userService.LockUserByEmailAsync(email);
            return user.Success ? Ok(user) : BadRequest();
        }

       //  [Authorize(Roles = "Admin")]
        [HttpPost("unlock")]
        public async Task<IActionResult> UnlockUser(string email)
        {
            var user = await _userService.UnlockUserByEmailAsync(email);
            return user.Success ? Ok(user) : BadRequest();
        }
       // [Authorize(Roles = "Admin")] 
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUserByEmail(string email)
        {
            var user = await _userService.DeleteUserByEmailAsync(email);
            return user.Success ? Ok(user) : BadRequest();
        }
       // [Authorize(Roles = "Admin")] 
        [HttpGet("All")]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userService.GetAllUsersAsync();
            return user.Success ? Ok(user) : BadRequest();
        }
    }
}
