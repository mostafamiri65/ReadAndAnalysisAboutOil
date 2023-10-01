using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadAndAnalysis.App.DTOs.News;
using ReadAndAnalysis.App.Extensions;
using ReadAndAnalysis.App.Services.Interfaces;

namespace ReadAndAnalysis.Web.Controllers
{
    [Authorize]
    public class InsertNewsController : BaseController
    {
        INewsService _newsService;
        public InsertNewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IActionResult> Index(string? startDate, string? endDate,int? estimateId)
        {
            var estimate = await _newsService.GetEstimateNews();
            ViewData["Estimates"] = estimate;
            var list = await _newsService.GetAllInsertedNews(startDate, endDate,estimateId);
            return View(list);
        }
        public async Task<IActionResult> CreateInserted()
        {
            var negative = await _newsService.GetNegativeReasons();
            ViewData["Reasons"] = negative;
            var date = await _newsService.GetLastPublishedDate(User.GetUserId());
            ViewBag.Date = date;
            var manage = await _newsService.GetManagements();
            ViewData["Manage"] = manage;
            var estimate = await _newsService.GetEstimateNews();
            ViewData["Estimates"] = estimate;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateInserted(CreateInsertedNewsDto create)
        {
            await _newsService.AddInsertedsNews(create, User.GetUserId());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowNews(long newsId)
        {
            var news = await _newsService.GetInsertedById(newsId);
            string management = await _newsService.GetManagementsTitle(Convert.ToInt64(news.ManagementSourceId));

            ViewBag.Mange = management;

            var negative = await _newsService.GetNegativeReasons();
            ViewData["Reasons"] = negative;

            string estimate;
            if (news.EstimateId == 1) { estimate = "خنثی"; }
            else if (news.EstimateId == 2) { estimate = "مثبت"; }
            else { estimate = "منفی"; }
            ViewBag.estimate = estimate;
            return View(news);
        }
        public async Task<IActionResult> Edit(long id)
        {
            var negative = await _newsService.GetNegativeReasons();
            ViewData["Reasons"] = negative;

            var news = await _newsService.GetInsertedById(id);
            var estimate = await _newsService.GetEstimateNews();
            ViewData["Estimates"] = estimate;
            var manage = await _newsService.GetManagements();
            ViewData["Manage"] = manage;
            return View(news);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(InsertedNewsListDto insert)
        {
            await _newsService.EditInsertedNews(insert, User.GetUserId());
            
            return RedirectToAction("Index");
        }
    }
}
