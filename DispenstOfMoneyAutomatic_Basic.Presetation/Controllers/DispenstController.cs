using Microsoft.AspNetCore.Mvc;

namespace DispenstOfMoneyAutomatic_Basic.Presetation.Controllers
{
    public class DispenstController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
