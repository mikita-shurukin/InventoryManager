using InventoryManager.DAL;
using InventoryManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemGroupsController : Controller
    {
        private readonly MainDbContext _context;

        public ItemGroupsController(MainDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var itemGroup = await _context.ItemGroups.AsNoTracking().ToListAsync();
            return View(itemGroup);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ItemGroup itemGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ItemGroups.Add(itemGroup);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Get)); // Перенаправляем на метод Get
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _context.ItemGroups.Remove(new ItemGroup { Id = id });

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemGroup itemGroup)
        {
            if (id != itemGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemGroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ItemGroupExists(int id)
        {
            return _context.ItemGroups.Any(e => e.Id == id);
        }

    }
}
