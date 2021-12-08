using Microsoft.AspNetCore.Mvc;

namespace Project_UIT247Green_User.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Order_history()
        {
            return View();
        }
        public IActionResult Order_information()
        {
            return View();
        }
    }
}
