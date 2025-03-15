using Microsoft.AspNetCore.Mvc;

namespace PRN222_Beverage_Website_Project.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
