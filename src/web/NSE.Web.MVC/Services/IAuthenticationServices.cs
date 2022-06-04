using NSE.Web.MVC.Models;
using System.Threading.Tasks;

namespace NSE.Identity.API.Services
{
    public interface IAuthenticationServices
    {
        Task<UserResponseLogin> Login(UserLogin usuarioLogin);
        Task<UserResponseLogin> Register(UserRegister usuarioRegister);
    }
}



