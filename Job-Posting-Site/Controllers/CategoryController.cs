using Job_Posting_Site.Models.AdViewModel;
using Job_Posting_Site.Models.CategoryViewModel;
using Job_Posting_Site.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Job_Posting_Site.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService categoryService;
        private readonly CommonService commonService;
        public CategoryController(CategoryService _categoryService, CommonService _commonService)
        {
            categoryService = _categoryService;
            commonService = _commonService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            CategoryFormModel model = new CategoryFormModel();
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(CategoryFormModel model)
        {
            model.Owner = commonService.FindUser(User);
            categoryService.Create(model);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!categoryService.CheckIfOwner(id, commonService.FindUser(User)))
            {
                return Unauthorized();
            }
            return View(categoryService.GetCategoryById(id));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(CategoryFormModel model)
        {
            categoryService.Update(model);
            return RedirectToAction("All", "category");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!categoryService.CheckIfOwner(id, commonService.FindUser(User)))
            {
                return Unauthorized();
            }
            return View(categoryService.GetCategoryById(id));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(CategoryFormModel model)
        {
            if (!categoryService.CheckIfOwner(model.Id, commonService.FindUser(User)))
            {
                return Unauthorized();
            }
            categoryService.Delete(model.Id);
            return RedirectToAction("All", "category");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var user = commonService.FindUser(User);
            ViewBag.Owner = user;
            var model = categoryService.GetCategoryById(id);
            ViewBag.Ads = categoryService.GetAllIdByCategoryId(id);
            
            return View(model);
        }
        [HttpGet]
        public IActionResult All() {
            var user = commonService.FindUser(User);
            ViewBag.Owner = null;
            if (user != null)
            {
                ViewBag.Role = commonService.FindRole(User).Name;
                ViewBag.Owner = user;
            }
            var categories = categoryService.GetAllCategories();
            ViewBag.Categories = categories;
            return View();

        }
    }
}
