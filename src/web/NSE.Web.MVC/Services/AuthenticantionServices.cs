using Microsoft.Extensions.Options;
using NSE.Web.MVC.Extensions;
using NSE.Web.MVC.Models;
using NSE.Web.MVC.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.Identity.API.Services
{
    public class AuthenticantionServices : Service, IAuthenticationServices
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;
        public AuthenticantionServices (HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            httpClient.BaseAddress = new Uri(_settings.AuthUrl);
        }

        public async Task<UserResponseLogin> Login(UserLogin userLogin)
        {
            var loginContent = GetContent(userLogin);
            var response = await _httpClient.PostAsync("/signIn", loginContent);
         
            if (!HandleErrorsReponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializerObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializerObjectResponse<UserResponseLogin>(response);
        }

        public async Task<UserResponseLogin> Register(UserRegister userRegister)
        {
            var registerContent = GetContent(userRegister);

            var response = await _httpClient.PostAsync("/signUp", registerContent);

            if (!HandleErrorsReponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult =
                       await DeserializerObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializerObjectResponse<UserResponseLogin>(response);
        }
    }

}



