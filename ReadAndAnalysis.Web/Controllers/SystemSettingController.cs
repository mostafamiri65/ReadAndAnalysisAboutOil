using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadAndAnalysis.App.Extensions;
using ReadAndAnalysis.App.Services.Interfaces;

namespace ReadAndAnalysis.Web.Controllers
{
    [Authorize]
    public class SystemSettingController : Controller
    {
        INewsService _newsService;
        public SystemSettingController(INewsService newsService)
        {
            _newsService = newsService;
        }
        public IActionResult AddMainKeys()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMainKeys(string title)
        {
            await _newsService.AddMainKeyWord(title,User.GetUserId());
            return Redirect("/News/KeyWords");
        }
    }
}
