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
            }
            else if (startDate != null && endDate != null)
            {
                start = startDate;
                end = endDate;
            }
            ViewBag.Start = start;
            ViewBag.EndDate = end;
            var notOilReasons = await _newsService.GetNotOilReasons();
            ViewData["NotOilReasons"] = notOilReasons;

            var list = await _newsService.GetAllOilNews(start, end);
            list = list.OrderBy(n => n.CreateDate).ToList();
            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> NotOilFromList(long newsId, int reasonId)
        {
            await _newsService.NotOilNewsWithReason(newsId, reasonId, User.GetUserId());
            return RedirectToAction("ShowOilNews");
        }
        public IActionResult NotOil(long newsId)
        {
            ViewBag.Id = newsId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NotOil(NotOil news)
        {
            await _newsService.DeleteOilNews(news.NewsId, User.GetUserId());
            return RedirectToAction("ShowOilNews");
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
            var notOilReasons = await _newsService.GetNotOilReasons();
            ViewData["NotOilReasons"] = notOilReasons;
            var smsTypes = await _newsService.GetSendingSmsTypes();
            ViewData["SmsTypes"] = smsTypes;
            var news = await _newsService.GetOilNews(newsId);

            return View(news);
        }
        [HttpPost]
        public async Task<IActionResult> NotOilReason(long newsId, int reasonId)
        {
            await _newsService.NotOilNewsWithReason(newsId, reasonId, User.GetUserId());
            return RedirectToAction("ShowOilNews");
        }
        [HttpPost]
        public async Task<IActionResult> Show(long newsId, Guid fieldParent, long fieldchild,
            long? source, long estimate, long relevance, int smsTypeId)
        {
            bool isexist = await _newsService.CanAddEvalutedResult(newsId);
            if (isexist)
            {
                TempData[ErrorMessage] = "این خبر پردازش شده است.";
                return RedirectToAction("ShowNews", new { newsId = newsId });
            }
            await _newsService.AddEvalutedResult(newsId, fieldchild, source, estimate,
                User.GetUserId(), relevance);
            if (smsTypeId != 0)
            {
                await _newsService.AddToNegativeOilNewsForSend(newsId, User.GetUserId(), smsTypeId);
            }

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
            ViewBag.Start = startDate;
            ViewBag.EndDate = endDate;
            var list = await _newsService.GetEvalutedNews(startDate, endDate, estimateId);
            list = list.OrderByDescending(l => l.EvalutedId
            ).ToList();
            var count = list.Count();
            ViewBag.Count = count;
            return View(list);
        }
        public async Task<IActionResult> SendSms()
        {
            await _newsService.SendingSms();
            return RedirectToAction("ShowEvaluteds");
        }
        [HttpGet]
        public async Task<IActionResult> ShowNegativeForSms(long newsId)
        {
            ViewBag.NewsId = newsId;
            var list = await _newsService.GetSendingSmsTypes();
            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> ShowNegativeForSms(long newsId, int typeId)
        {

            await _newsService.AddToNegativeOilNewsForSend(newsId, User.GetUserId(), typeId);
            return RedirectToAction("ShowEvaluteds");
        }
        [HttpGet("SelectedNews")]
        public async Task<IActionResult> SelectedNews()
        {
            var list = await _newsService.GetSelectedNewsTypes();
            return View(list);
        }
        public async Task<IActionResult> SendNexLevel(long id)
        {
            await _newsService.SendNextLevel(id, User.GetUserId());
            TempData[SuccessMessage] = "به مرحله بعدی ارسال شد";
            return RedirectToAction("SelectedNews");
        }
        public async Task<IActionResult> ViewWithNoAction(long id)
        {
            await _newsService.ViewWithNoAction(id, User.GetUserId());
            TempData[SuccessMessage] = "مشاهده شد و اقدامی ندارد";
            return RedirectToAction("SelectedNews");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveOilN(List<long> checkedId,int typeReasonId)
        {
            foreach(var item in checkedId)
            {
                await _newsService.NotOilNewsWithReason(item, typeReasonId, User.GetUserId());
               
            }
            return RedirectToAction("ShowOilNews");
        }
        [AllowAnonymous]

        public async Task<IActionResult> SNK(string key)
        {
            //await _newsService.SendNextLevelWithUrl(key);
            var isSend = await _newsService.IsNewsSendedSms(key);
            ViewBag.IsSend = isSend;
            var result = await _newsService.GetNewsByKey(key);
            ViewBag.Key = key;
            return View(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SNK(string key, int type)
        {
            if (type == 1)
                await _newsService.SendNextLevelWithUrl(key);

            return View("DoNothing");
        }



        public async Task<IActionResult> SendAsPrivate()
        {
            var types = await _newsService.GetSendingSmsTypes();
            ViewData["Types"] = types;
            var relevances = await _newsService.GetRelevances();
            ViewData["Relevances"] = relevances;
            var fields = await _newsService.GetFieldOfUse();
            ViewData["fields"] = fields;
            var estimate = await _newsService.GetEstimateNews();
            ViewData["estimate"] = estimate;
            var source = await _newsService.GetStatisticalSource();
            ViewData["source"] = source;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendAsPrivate(CreateNewsAsPrivateDto create, long fieldchild)
        {
            if ((!create.LinkUrl.StartsWith("https://")) || (!create.LinkUrl.StartsWith("http://")))
                create.LinkUrl = "https://" + create.LinkUrl;

            var date = create.PublishedDate.Split("/");
            int month = Convert.ToInt32(date[1]);
            int day;
            if (Convert.ToInt32(date[2]) > 0 && Convert.ToInt32(date[2]) < 32)
                day = Convert.ToInt32(date[2]);
            else
                day = Convert.ToInt32(date[0]);

            int year;
            if (date[2].Length == 4)
                year = Convert.ToInt32(date[2]);
            else year = Convert.ToInt32(date[0]);


            string published = day + "/" + month + "/" + year;
            create.PublishedDate = published;

            create.FieldOfUseId = fieldchild;
            await _newsService.CreateNewsAsPrivateMode(create, User.GetUserId());
            var types = await _newsService.GetSendingSmsTypes();
            ViewData["Types"] = types;
            var relevances = await _newsService.GetRelevances();
            ViewData["Relevances"] = relevances;
            var fields = await _newsService.GetFieldOfUse();
            ViewData["fields"] = fields;
            var estimate = await _newsService.GetEstimateNews();
            ViewData["estimate"] = estimate;
            var source = await _newsService.GetStatisticalSource();
            ViewData["source"] = source;

            return RedirectToAction("ShowEvaluteds");
        }

        public async Task<IActionResult> ShowLikes(long newsId)
        {
            ViewBag.newsId = newsId;
            var list = await _newsService.GetLikeNews(newsId);
            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> ShowLikes(List<long> ids, long newsId)
        {
            await _newsService.UpdateLikeNews(newsId, ids, User.GetUserId());
            return RedirectToAction("ShowEvaluteds");

        }
    }
}
