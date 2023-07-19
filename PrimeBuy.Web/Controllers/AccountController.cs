using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrimeBuy.Application.Services.Interfaces;
using PrimeBuy.Application.ViewModels;

namespace PrimeBuy.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginModel model)
        {
            try
            {
                if(User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
                var user = await _accountService.Login(model);
                if(user == null){
                    TempData["ErrorMessage"] = "Invalid username and/or password";
                    return View();
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                };
                var roles = await _accountService.GetUserRoles(model.UserName);
                foreach(var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync("Cookies", principal).Wait();

                return RedirectToAction("Index", "Home");
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        public IActionResult Register()
        {
            try
            {
                 if(User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");
                return View();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] AccountRegisterModel model)
        {
            try
            {
                if(User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
                var response = await _accountService.Register(model);
                if(response == false)
                {
                    TempData["ErrorMessage"] = "Error while creating user";
                }
                var accountLoginModel = new AccountLoginModel{UserName = model.UserName, Password = model.Password};
                return RedirectToAction("Login", "Account", accountLoginModel);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            Response.Cookies.Delete("Cart");
            Response.Cookies.Append("Cart", "");
            return RedirectToAction("Index", "Home");
        }
    }
}