using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendSmsByKey.Entities;
using SendSmsByKey.Models;
using System.Diagnostics;

namespace SendSmsByKey.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TxtPrc2Context _context;
        public HomeController(ILogger<HomeController> logger, TxtPrc2Context context)
        {
            _logger = logger;
            _context = context;
        }
        [Route("{key}")]
       public async Task<IActionResult> SendSms(string key)
        {
            var res = await _context.NegativeOilNewsForSendingSms.SingleOrDefaultAsync(r =>
           r.SendedLevel == 0 && r.UniqueCode == key);
            if (res != null)
            {
                res.SendedLevel = 1;
                res.SendDate = DateTime.Now;
                _context.NegativeOilNewsForSendingSms.Update(res);
                await _context.SaveChangesAsync();
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
