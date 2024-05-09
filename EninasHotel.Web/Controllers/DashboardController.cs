using Microsoft.AspNetCore.Mvc;

namespace EninasHotel.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
