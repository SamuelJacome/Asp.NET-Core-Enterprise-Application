using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using NSE.Web.MVC.Models;
using NSE.Web.MVC.Services;

namespace NSE.Identity.API.Services
{
    public class AuthenticantionServices : Service, IAuthenticationServices
    {
        private readonly HttpClient _httpClient;
        public AuthenticantionServices (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserResponseLogin> Login(UserLogin userLogin)
        {
            var loginContent = new StringContent(JsonSerializer.Serialize(userLogin), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:44319/signIn", loginContent);
            var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
            if (!HandleErrorsReponse(response))
            {
                var teste = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options);
                return new UserResponseLogin
                {
                    ResponseResult = 
                        JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

                return JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<UserResponseLogin> Register(UserRegister userRegister)
        {
            var registerContent = new StringContent(JsonSerializer.Serialize(userRegister), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:44319/signUp", registerContent);
            var test = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            if (!HandleErrorsReponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult =
                        JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync(), options);
        }
    }

}



