using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadAndAnalysis.App.DTOs.News;
using ReadAndAnalysis.App.Extensions;
using ReadAndAnalysis.App.Services.Interfaces;


namespace ReadAndAnalysis.Web.Controllers
{
    [Authorize]
    public class NewsController : BaseController
    {
        private INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;

        }
        public async Task<IActionResult> ShowOilNews(int? date, string? startDate, string? endDate)
        {
            string start = "";
            string end = "";
            if (date == null && startDate == null && endDate == null)
            {
                start = DateExtensions.FirstDayOfPersianMonth();
                end = DateTime.Now.ToShamsi();
             
            }
            if (date != null)
            {
                start = DateTime.Now.AddDays((double)-date).ToShamsi();
                end = DateTime.Now.ToShamsi();
            }else if(startDate!=null && endDate != null)
            {
                start = startDate;
                end = endDate;
            }
            ViewBag.Start = start;
            ViewBag.EndDate = end;
            var list = await _newsService.GetAllOilNews(start, end);
            list = list.OrderBy(n => n.CreateDate).ToList();
            return View(list);
        }
        public async Task<IActionResult> ShowNews(long newsId)
        {
            var keywords = await _newsService.GetAllUsedKeyWords(newsId);
            ViewData["Words"] = keywords;
            var relevances = await _newsService.GetRelevances();
            ViewData["Relevances"] = relevances;

            ViewBag.IsEvaluated = await _newsService.IsEvaluated(newsId);
            var evaluted = await _newsService.GetEvaluatedResult(newsId);
            ViewData["evaluted"] = evaluted;
            var fields = await _newsService.GetFieldOfUse();
            ViewData["fields"] = fields;
            var estimate = await _newsService.GetEstimateNews();
            ViewData["estimate"] = estimate;
            var source = await _newsService.GetStatisticalSource();
            ViewData["source"] = source;
            var title = await _newsService.GetEvalutedFieldTitle(newsId);
            ViewData["titleEva"] = title;
            var news = await _newsService.GetOilNews(newsId);
            return View(news);
        }
        [HttpPost]
        public async Task<IActionResult> Show(long newsId, Guid fieldParent, long fieldchild,
            long? source, long estimate, long relevance)
        {
            bool isexist = await _newsService.CanAddEvalutedResult(newsId);
            if (!isexist)
            {
                TempData[ErrorMessage] = "این خبر پردازش شده است.";
                return RedirectToAction("ShowNews", new { newsId = newsId });
            }
            await _newsService.AddEvalutedResult(newsId, fieldchild, source, estimate,
                User.GetUserId(), relevance);
            return RedirectToAction("ShowOilNews");
        }
        public async Task<IActionResult> KeyWords()
        {
            var list = await _newsService.GetAllMainKeyWords();
            return View(list);
        }

        public IActionResult AddMainKeys()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMainKeys(string title)
        {
            await _newsService.AddMainKeyWord(title, User.GetUserId());
            return RedirectToAction("KeyWords");
        }
        public async Task<IActionResult> ShowNewsByMainKey(long mainId)
        {
            var list = await _newsService.ShowOilNewsByMainKey(mainId);
            return View(list);
        }

        public async Task<IActionResult> GetFieldOfUse(long parent)
        {
            var list = await _newsService.GetChildFieldOfUse(parent);
            return PartialView(list);
        }
        public async Task<IActionResult> DeleteEvaluted(long newsId)
        {
            var news = await _newsService.GetOilNews(newsId);
            return PartialView(news);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteEvaluted(OilNewsDto news)
        {
            await _newsService.DeleteEvaluated(news.Id);
            return RedirectToAction("ShowNews", new { newsId = news.Id });
        }
        [HttpGet]
        public async Task<IActionResult> ShowEvaluteds(string? startDate, string? endDate, int? estimateId)
        {
            var estimate = await _newsService.GetEstimateNews();
            ViewData["estimate"] = estimate;
            var list = await _newsService.GetEvalutedNews(startDate, endDate, estimateId);
            return View(list);
        }
    }
}
