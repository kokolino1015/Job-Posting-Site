using Job_Posting_Site.Data;
using Job_Posting_Site.Data.Entities;
using Job_Posting_Site.Data.Entities.Account;
using Job_Posting_Site.Models;
using Job_Posting_Site.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Job_Posting_Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;
        public AccountController(
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
            ApplicationDbContext context)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            this.context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            ViewBag.Roles = context.Roles.Where(r => r.Id > -1).Select(x => new Role
            {
                Id = x.Id,
                Name = x.Name,
            });
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                EmailConfirmed = true,
                LastName = model.LastName,
                UserName = model.Email,
                Role = context.Roles.Where(r => r.Id == model.Role).FirstOrDefault()
            };

            var result = await userManager.CreateAsync(user, model.Password);


            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }


            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {

                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    if (model.ReturnUrl != null)
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}