using AutoMapper;
using InventoryManager.DAL;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
