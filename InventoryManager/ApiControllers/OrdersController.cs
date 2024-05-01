using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.ApiControllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
