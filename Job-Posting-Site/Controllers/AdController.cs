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
           if(commonService.FindRole(User).Name != "employer")
            {
                return Unauthorized();
            }
            AdFormModel model = new AdFormModel();
            ViewBag.Categories = adService.GetCategories();
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(AdFormModel model)
        {
            if (commonService.FindRole(User).Name != "employer")
            {
                return Unauthorized();
            }
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
            if (commonService.FindRole(User).Name != "employer")
            {
                return Unauthorized();
            }
            adService.Update(model);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Details(int id) 
        {
            var user = commonService.FindUser(User);
            ViewBag.Owner = null;
            if (user != null) {
                ViewBag.Role = commonService.FindRole(User).Name;
                ViewBag.Owner = user;
            }
            ViewBag.Show = false;
            var model = adService.GetAdById(id);
            if (adService.checkifUserInCandidateList(model, user) == "unapplied")
            {
                ViewBag.Show = true; 
            }
            
            ViewBag.Employer = false;
            if (adService.checkifUserInCandidateList(model, user) == "employer")
            {
                ViewBag.Employer = true;
            }
            ViewBag.AdId = id;
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
            if(commonService.FindRole(User).Name == "employer")
            {
                return RedirectToAction("Index", "Home");
            }
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
