using AutoMapper;
using InventoryManager.DAL;
using InventoryManager.DAL.Models;
using InventoryManager.Models;
using InventoryManager.Models.Requests.Warehouse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : Controller
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public WarehousesController(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var warehouse = await _context.Warehouses.AsNoTracking().ToListAsync();
            return View(warehouse);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateWarehouseRequest request)
        {

            var warehouseMap = _mapper.Map<Warehouse>(request);
            _context.Warehouses.Add(warehouseMap);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _context.Warehouses.Remove(new Warehouse { Id = id });

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}/ShowItems")]
        public async Task<IActionResult> ShowItems(int id)
        {
            var itemsInWarehouse = await _context.InventoryItems
                .Include(ii => ii.Item)
                .ThenInclude(i => i.Group)
                .Where(ii => ii.WarehouseId == id)
                .ToListAsync();

            ViewBag.WarehouseId = id; // Установка значения WarehouseId

            return View("WarehouseItems", itemsInWarehouse);
        }

        [HttpGet("AddItems")]
        public IActionResult AddItems()
        {
            return View();
        }

        [HttpPost("AddItems")]
        public async Task<IActionResult> AddItems(AddItemRequest request)
        {

            if (!await _context.Warehouses.AsNoTracking().AnyAsync(x => x.Id == request.WarehouseId))
                return NotFound($"Warehouse with id={request.WarehouseId} was not found");

            var itemIds = request.ItemPositions.Select(x => x.ItemId).Distinct().ToList();

            if (await _context.Items.AsNoTracking().Where(x => itemIds.Contains(x.Id)).CountAsync() != itemIds.Count)
                return NotFound($"Items with some id from request was not found");

            if (request.ItemPositions.Any(x => x.Quantity <= 0))
                return BadRequest("Quantity has to be set more than zero");


            var itemPositions = request.ItemPositions.GroupBy(x => x.ItemId).Select(x => new ItemPosition
            {
                UnitOfMeasurement = x.First().UnitOfMeasurement,
                ItemId = x.Key,
                Quantity = x.Select(a => a.Quantity).Aggregate((a, b) => a + b)
            });

            foreach (var itemPosition in itemPositions)
            {
                var existingInventoryItem = await _context.InventoryItems
                    .AsNoTracking()
                    .Where(x => x.WarehouseId == request.WarehouseId)
                    .FirstOrDefaultAsync(x => x.ItemId == itemPosition.ItemId);

                if (existingInventoryItem != null)
                {
                    existingInventoryItem.Quantity += itemPosition.Quantity;
                }
                else
                {
                    _context.InventoryItems.Add(new InventoryItem
                    {
                        ItemId = itemPosition.ItemId,
                        Quantity = itemPosition.Quantity,
                        UnitOfMeasurement = itemPosition.UnitOfMeasurement,
                        WarehouseId = request.WarehouseId
                    });
                }
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
