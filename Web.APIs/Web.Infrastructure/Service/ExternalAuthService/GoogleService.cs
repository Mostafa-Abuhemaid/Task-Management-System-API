using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.AccountDTO;
using Web.Application.Interfaces;
using Web.Application.Interfaces.ExternalAuthService;
using Web.Application.Response;
using Web.Domain.Entites;

namespace Web.Infrastructure.Service.ExternalAuthService
{
    public class GoogleService : IGoogleService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        public GoogleService(IConfiguration configuration, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<BaseResponse<TokenDTO>> GoogleSignInAsync(string TokenId)
        {
            var payload = await VerifyGoogleToken(TokenId);
            if (payload == null)
                return new BaseResponse<TokenDTO>(false, "Failed to validate Google ID token");

            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = payload.Email,
                    Email = payload.Email
                };
                await _userManager.CreateAsync(user);
            }

            var res = new TokenDTO
            {
                UserId = user.Id,
                Email = user.Email,

                Name = user.UserName,
                Token = await _tokenService.GenerateTokenAsync(user, _userManager)
            };

            return new BaseResponse<TokenDTO>(true, "تم تسجيل الدخول بنجاح", res);

        }
        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _configuration["Authentication:Google:ClientId"] }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                return payload;
            }
            catch
            {
                return null;
            }
        }
    }
}
