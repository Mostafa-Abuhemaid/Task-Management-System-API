using Azure.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Web.Application.DTOs.AccountDTO;
using Web.Application.Interfaces;
using Web.Application.Interfaces.ExternalAuth;
using Web.Application.Response;
using Web.Domain.Entites;

namespace Web.Infrastructure.Service.ExternalAuthService
{
    public class FacebookService : IFacebookService
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        public FacebookService(HttpClient httpClient, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<BaseResponse<TokenDTO>> FacebookSignInAsync(string accessToken)
        {
           
            var httpClient = new HttpClient();
            var verifyTokenUrl = $"https://graph.facebook.com/me?access_token={accessToken}&fields=id,name,email";
            var response = await httpClient.GetAsync(verifyTokenUrl);

            if (!response.IsSuccessStatusCode)
                return new BaseResponse<TokenDTO>(false, "فشل التحقق من Facebook token");

            var content = await response.Content.ReadAsStringAsync();
            var facebookUser = JsonSerializer.Deserialize<SignInGoogleDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (facebookUser == null || string.IsNullOrEmpty(facebookUser.Email))
                return new BaseResponse<TokenDTO>(false, "لم يتم العثور على بريد إلكتروني في حساب فيسبوك");

            
            var user = await _userManager.FindByEmailAsync(facebookUser.Email);
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = facebookUser.Email,
                    Email = facebookUser.Email
                };
                await _userManager.CreateAsync(user);
            }

            var token = await _tokenService.GenerateTokenAsync(user, _userManager);

            var res = new TokenDTO
            {
                UserId = user.Id,
                Email = user.Email,
                Name = facebookUser.Name,
                Token = token
            };

            return new BaseResponse<TokenDTO>(true, "تم تسجيل الدخول عبر Facebook بنجاح", res);
        }

    }
}
