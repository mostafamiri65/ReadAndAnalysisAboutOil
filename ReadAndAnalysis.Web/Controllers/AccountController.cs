using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ReadAndAnalysis.App.Services.Interfaces;
using System.Security.Claims;
using ReadAndAnalysis.App.DTOs.Accounting;

namespace ReadAndAnalysis.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Ctor
        private IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        #endregion

        #region LogIn

        [HttpGet("Login")]
        public IActionResult Login(string returnUrl = "")
        {
            var result = new LoginUserDto();
            if (!string.IsNullOrEmpty(returnUrl))
            {
                result.ReturnUrl = returnUrl;
            }
            return View(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            if (!string.IsNullOrEmpty(login.Username) && !string.IsNullOrEmpty(login.Password))
            {
                var user = await _accountService.GetUserForLogin(login.Username,login.Password);
                if (user.Id == 0)
                {
                    var result = new LoginUserDto();
                    if (!string.IsNullOrEmpty(login.ReturnUrl))
                    {
                        result.ReturnUrl = login.ReturnUrl;
                    }
                    TempData[ErrorMessage] = "اطلاعات وارده اشتباه است";
                    return View(login);
                }

               else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                      

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };

                    await HttpContext.SignInAsync(principal, properties);
                    if (await _accountService.IsSecretary(user.Id))
                        return RedirectToAction("Index", "InsertNews");
                }
            }

            if (!string.IsNullOrEmpty(login.ReturnUrl))
            {
                return Redirect(login.ReturnUrl);
            }
            return Redirect("/");
        }

        #endregion

        #region Logout

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }


        #endregion
    }
}
