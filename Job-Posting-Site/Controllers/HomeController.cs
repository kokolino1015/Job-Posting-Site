using Job_Posting_Site.Data.Entities;
using Job_Posting_Site.Models;
using Job_Posting_Site.Models.AdViewModel;
using Job_Posting_Site.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Job_Posting_Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AdService adService;
        private readonly CommonService commonService;
        public HomeController(ILogger<HomeController> logger, AdService adService, CommonService commonService)
        {
            _logger = logger;
            this.commonService = commonService;
            this.adService = adService;
        }

        public IActionResult Index()
        {
            List<AdFormModel> ads = adService.GetFirst10Ads();
            var role = commonService.FindRole(User);
            if (role != null)
            {
                ViewBag.Role = commonService.FindRole(User).Name;
            }
            ViewBag.Ads = ads;
            ViewBag.Owner = commonService.FindUser(User);
            ViewBag.Categories = adService.GetCategories();
            return View();
        }

        public IActionResult Privacy()
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