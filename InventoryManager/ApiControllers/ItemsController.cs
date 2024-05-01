using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.ApiControllers
{
    public class ItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
