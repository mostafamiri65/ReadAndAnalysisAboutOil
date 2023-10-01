using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadAndAnalysis.App.DTOs.Accounting;
using ReadAndAnalysis.App.Extensions;
using ReadAndAnalysis.App.Services.Interfaces;

namespace ReadAndAnalysis.Web.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private IAccountService _accountService;
        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            bool hasAccess = await _accountService.HasAdminAccess(User.GetUserId());
            if (!hasAccess)
            {
                TempData[ErrorMessage] = "شما اجازه دسترسی به این قسمت را ندارید";
                return Redirect("/");
            }
            var users = await _accountService.GetAllUsers();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            bool hasAccess = await _accountService.HasAdminAccess(User.GetUserId());
            if (!hasAccess)
            {
                TempData[ErrorMessage] = "شما اجازه دسترسی به این قسمت را ندارید";
                return Redirect("/");
            }
            var roles = await _accountService.GetAllRoles();
            ViewData["Roles"] = roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto create, List<long> roleIds)
        {
            create.RoleIds = roleIds;
            var user = await _accountService.CreateUser(create);
            if (!user)
            {
                TempData[ErrorMessage] = "مشکلی پیش آمده است";
                var roles = await _accountService.GetAllRoles();
                ViewData["Roles"] = roles;
                return View();
            }
            TempData[SuccessMessage] = "کاربر ایجاد شد";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword(long userId)
        {
            bool hasAccess = await _accountService.HasAdminAccess(User.GetUserId());
            if (!hasAccess)
            {
                TempData[ErrorMessage] = "شما اجازه دسترسی به این قسمت را ندارید";
                return Redirect("/");
            }
            var user = await _accountService.GetUserById(userId);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(UsersDto dto,string password)
        {
            var user = await _accountService.ChangePassword(dto.Id,password,User.GetUserId());
            return RedirectToAction("Index");
        }
    }
}
