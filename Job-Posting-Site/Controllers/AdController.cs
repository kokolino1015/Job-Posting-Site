using Microsoft.AspNetCore.Mvc;

namespace Job_Posting_Site.Controllers
{
    public class AdController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
