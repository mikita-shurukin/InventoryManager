using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.ApiControllers
{
    public class WarehousesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
