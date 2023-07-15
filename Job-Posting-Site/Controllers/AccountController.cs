using Microsoft.AspNetCore.Mvc;

namespace Job_Posting_Site.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
