using Job_Posting_Site.Data.Entities.Account;
using Job_Posting_Site.Models.AdViewModel;
using Job_Posting_Site.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Job_Posting_Site.Controllers
{
    public class AdController : Controller
    {
        private readonly AdService adService;
        private readonly CommonService commonService;
        public AdController(AdService adService, CommonService commonService)
        {
            this.commonService = commonService;
            this.adService = adService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            AdFormModel model = new AdFormModel();
            ViewBag.Owner = commonService.FindUser(User);
            ViewBag.Categories = adService.GetCategories();
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(AdFormModel model)
        {
            adService.Create(model);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!adService.CheckIfOwner(id, commonService.FindUser(User)))
            {
                return Unauthorized();
            }
            this.ViewBag.Owner = commonService.FindUser(User);
            return View(adService.GetAdById(id));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(AdFormModel model)
        {
            adService.Update(model);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            ViewBag.Role = commonService.FindUser(User).Role.Name;
            return View(adService.GetAdById(id));
        }
    }
}