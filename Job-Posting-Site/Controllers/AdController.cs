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
        private readonly AdService adService ;
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
            ViewBag.Categories = adService.GetCategories();
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(AdFormModel model)
        {
            model.Owner = commonService.FindUser(User);
            adService.Create(model);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(!adService.CheckIfOwner(id, commonService.FindUser(User)))
            {
                return Unauthorized();
            }
            ViewBag.Categories = adService.GetCategories();
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
            var user = commonService.FindUser(User);
            var role = commonService.FindRole(User);
            if(role != null) {
                ViewBag.Role = commonService.FindRole(User).Name;
            }
            ViewBag.Show = false;
            var model = adService.GetAdById(id);
            if (adService.checkifUserInCandidateList(model, user))
            {
                ViewBag.Show = true; 
            }                   
            ViewBag.Category = adService.GetCategoryById(model.Category).Name;
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!adService.CheckIfOwner(id, commonService.FindUser(User)))
            {
                return Unauthorized();
            }
            return View(adService.GetAdById(id));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(AdFormModel model)
        {
            if (!adService.CheckIfOwner(model.Id, commonService.FindUser(User)))
            {
                return Unauthorized();
            }
            adService.Delete(model.Id);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Apply(int  id)
        {
            adService.ApplyUser(commonService.FindUser(User), adService.GetAdById(id));
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Unapply(int id)
        {
            adService.UnapplyUser(commonService.FindUser(User), adService.GetAdById(id));
            return RedirectToAction("Index", "Home");
        }
    }
}
