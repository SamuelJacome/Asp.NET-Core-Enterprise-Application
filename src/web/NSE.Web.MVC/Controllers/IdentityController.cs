using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NSE.Identity.API.Services;
using NSE.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Controllers
{
    public class IdentityController : MainController
    {
        private readonly IAuthenticationServices _authenticationServices;

        public IdentityController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);

            //Api - Registro

            var response = await _authenticationServices.Register(userRegister);

            if (HasResponseErrors(response.ResponseResult)) return View(userRegister);

            //Realizar login na APP
            await StartSession(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return View(userLogin);

            //Api - Login

            var response = await _authenticationServices.Login(userLogin);

            if (HasResponseErrors(response.ResponseResult)) return View(userLogin);

            //Realizar login na APP
            await StartSession(response);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout(UserLogin userLogin)
        {
            return RedirectToAction("Index", "Home");
        }

        private async Task StartSession (UserResponseLogin response)
        {
            var token = GetTokenFormated(response.AccessToken);
            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", response.AccessToken));
            claims.AddRange(token.Claims);

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimIdentity),
                    authProperties);
        }

        private static JwtSecurityToken GetTokenFormated (string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }


    }
}
