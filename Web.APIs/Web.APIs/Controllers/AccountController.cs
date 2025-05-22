using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using Web.Application.DTOs;
using Web.Application.DTOs.AccountDTO;
using Web.Application.Interfaces;
using Web.Application.Interfaces.ExternalAuth;
using Web.Application.Interfaces.ExternalAuthService;
using Web.Application.Response;
using Web.Infrastructure.Service;

namespace Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IGoogleService _googleService;
        private readonly IFacebookService _facebookService;
        public AccountController(IAccountService accountService, IGoogleService googleService, IFacebookService facebookService)
        {

            _accountService = accountService;
            _googleService = googleService;
            _facebookService = facebookService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<BaseResponse<TokenDTO>>> Login(RegisterDto registerDto)
        {
            var result = await _accountService.RegisterAsync(registerDto);
            return result.Success ? Ok(result) : BadRequest(result);

        }
        [HttpPost("Login")]
        public async Task<ActionResult<BaseResponse<TokenDTO>>> Login(LoginDTO loginDto)
        {
            var result = await _accountService.LoginAsync(loginDto);
            return result.Success ? Ok(result) : BadRequest(result);

        }
        [HttpPost("ForgetPassword")]
        public async Task<ActionResult<BaseResponse<string>>> ForgetPassword([FromBody] ForgetPasswordDto request)
        {

            var result = await _accountService.ForgotPasswordAsync(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("VerifyOTP")]
        public async Task<ActionResult<BaseResponse<bool>>> VerifyOTP([FromBody] VerfiyCodeDto verify)
        {

            var result = await _accountService.VerifyOTPAsync(verify);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            var result = await _accountService.ResetPasswordAsync(resetPassword);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin( string idToken)
        {
            var result = await _googleService.GoogleSignInAsync(idToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("Facebook-login")]
        public async Task<IActionResult> FascebookLogin(string idToken)
        {
            var result = await _facebookService.FacebookSignInAsync(idToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<ActionResult<BaseResponse<bool>>> ChangePassword([FromBody] ChangePasswordDto request)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _accountService.ChangePasswordAsync(userId, request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}
