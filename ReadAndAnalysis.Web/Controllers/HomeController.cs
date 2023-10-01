﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReadAndAnalysis.App.Extensions;
using ReadAndAnalysis.App.Services.Interfaces;
using ReadAndAnalysis.Web.Models;
using System.Diagnostics;

namespace ReadAndAnalysis.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IHomeService _homeService;
        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index(string? start , string end , int? relevanceId)
        {
            var isSecrotary = await _homeService.IsSecretory(User.GetUserId());
            if(isSecrotary)
            {
                return RedirectToAction("Index", "InsertNews");
            }

            var boss = await _homeService.IsBoss(User.GetUserId());
            ViewBag.IsBoss = boss;
            if(boss)
            {
                ViewData["Relevances"] = await _homeService.GetNewsRelevance();
                if (start == null) start = DateTime.Now.ToShamsi();
                if(end == null) end = (DateTime.Now.AddDays(1)).ToShamsi();
                if(relevanceId==null) relevanceId = 1;
                ViewBag.RelevanceTitle = await _homeService.GetRelevanceTitleById((int)relevanceId);
                var negative = await _homeService.GetNegativeOilNewsCount(start, end);
                var posetive = await _homeService.GetPosetiveOilNewsCount(start , end);
                var neutral = await _homeService.GetPosetiveOilNewsCount(start, end);
                var relevance = await _homeService.GetEvaluatedNewsByBoos(start, end,relevanceId);
                ViewBag.Negative = negative;
                ViewBag.Posetive = posetive;
                ViewBag.Neutral = neutral;
                ViewBag.Relevance = relevance;
            }
            var sp = await _homeService.GetDataFromSp();
            var count = await _homeService.GetAllNewsCount();
            ViewBag.Count = count;
            ViewBag.GetUsersCount = await _homeService.GetUsersCount();
            ViewBag.GetOurNewsCount = await _homeService.GetOilNewsCount();
            ViewBag.GetProcessedNewsCount = Convert.ToInt64(await _homeService.GetProcessedNewsCount());
            ViewData["TestSp"] = sp;
          

            return View();
        }
        public async Task<IActionResult> GetChart()
        {
            var sp = await _homeService.GetDataFromSp();
            ViewData["TestSp"] = sp;
            return View();
        }

        public async Task<IActionResult> ShowResult()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}