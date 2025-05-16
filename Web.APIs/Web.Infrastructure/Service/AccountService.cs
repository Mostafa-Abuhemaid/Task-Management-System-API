using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Web.Application.DTOs.AccountDTO;
using Web.Application.Files;
using Web.Application.Interfaces;
using Web.Application.Response;
using Web.Domain.Entites;
using Web.Domain.Enums;

namespace Web.Infrastructure.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;    
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _memoryCache;
       
        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, ITokenService tokenService, IMapper mapper, IMemoryCache memoryCache, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
            _tokenService = tokenService;         
            _memoryCache = memoryCache;
            _emailService = emailService;
        }

        public async Task<BaseResponse<string>> ForgotPasswordAsync(ForgetPasswordDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new BaseResponse<string>(false, "لم يتم العثور على بريدك الإلكتروني");

            var otp = new Random().Next(100000, 999999).ToString();
            _memoryCache.Set(request.Email, otp, TimeSpan.FromMinutes(60));
            await _emailService.SendEmailAsync(request.Email, "Smile-Simulation", $"Your VerifyOTP code is: {otp}");
            var Token = await _userManager.GeneratePasswordResetTokenAsync(user); 
            return new BaseResponse<string>(true, "تحقق من بريدك الاكتروني", Token);
        }

        public async Task<BaseResponse<TokenDTO>> LoginAsync(LoginDTO loginDto)
        {
            if (loginDto == null)
                return new BaseResponse<TokenDTO>(false, "Login information is requird");

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return new BaseResponse<TokenDTO>(false, "The Email or Password in not correct");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return new BaseResponse<TokenDTO>(false, "The Email or Password in not correct");
            var roles = await _userManager.GetRolesAsync(user);
            var res = new TokenDTO
            {
                UserId = user.Id,
                Email = user.Email,
               
              Name=user.UserName,
                Token = await _tokenService.GenerateTokenAsync(user, _userManager)
            };

            return new BaseResponse<TokenDTO>(true, "Login succesfuly", res);
        }

        public async Task<BaseResponse<TokenDTO>> RegisterAsync(RegisterDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
                return new BaseResponse<TokenDTO>(false, "Password and confirmation password do not match.");

            if (!new EmailAddressAttribute().IsValid(registerDto.Email))
                return new BaseResponse<TokenDTO>(false, "Invalid email format.");

            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
                return new BaseResponse<TokenDTO>(false, "A user with this email already exists.");

            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$";
            if (!Regex.IsMatch(registerDto.Password, passwordPattern))
            {
                return new BaseResponse<TokenDTO>(false, "Password must contain uppercase and lowercase letters, numbers, and special characters.");
            }

            var user = new AppUser
            {
                UserName = registerDto.Name,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return new BaseResponse<TokenDTO>(false, "Failed to create account.");

            await _userManager.AddToRoleAsync(user, Roles.User.ToString());

            var response = new TokenDTO
            {
               
                Name=registerDto.Name,
                Email = registerDto.Email,
                Token = await _tokenService.GenerateTokenAsync(user, _userManager)
            };

            return new BaseResponse<TokenDTO>(true, "Account created successfully.", response);
        }


        public async Task<BaseResponse<bool>> ResetPasswordAsync(ResetPasswordDto resetPassword)
        {
            if (resetPassword.NewPassword != resetPassword.ConfirmNewPassword)
                return new BaseResponse<bool>(false, "كلمة المرور وتأكيد كلمة المرور لا يتطابقان");

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null) return new BaseResponse<bool>(false, "لم يتم العثور على بريدك الإلكتروني");

            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.NewPassword);
            if (!result.Succeeded) return new BaseResponse<bool>(false, "فشلت إعادة تعيين كلمة المرور");

            return new BaseResponse<bool>(true, "تم تحديث كلمة المرور بنجاح");
        }

        public async Task<BaseResponse<bool>> VerifyOTPAsync(VerfiyCodeDto verify)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(verify.Email);
                if (user == null)
                    return new BaseResponse<bool>(false, $"Email '{verify.Email}' is not found.");

                var cachedOtp = _memoryCache.Get(verify.Email)?.ToString();
                if (string.IsNullOrEmpty(cachedOtp))
                    return new BaseResponse<bool>(false, "لم يتم العثور على الرمز أو انتهت صلاحيته. يرجى طلب رمز جديد.");

                if (!string.Equals(verify.CodeOTP, cachedOtp, StringComparison.OrdinalIgnoreCase))
                    return new BaseResponse<bool>(false, "الرمز غير صحيح. تأكد من إدخاله بشكل صحيح.");

            
                _memoryCache.Remove(verify.Email);

                return new BaseResponse<bool>(true, "تم التحقق من الرمز بنجاح.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in VerifyOTPAsync: {ex.Message}");
                return new BaseResponse<bool>(false, "حدث خطأ في السيرفر، حاول مرة أخرى لاحقًا.");
            }

        }
    }
}
