using AutoMapper;
using InventoryManager.DAL;
using InventoryManager.DAL.Models;
using InventoryManager.Models.Requests.ItemGroup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemGroupsController : Controller
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public ItemGroupsController(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<IActionResult> Create([FromForm] CreateItemGroupRequest request)
        {
            var newItemGroup = _mapper.Map<ItemGroup>(request);
            _context.ItemGroups.Add(newItemGroup);

            await _context.SaveChangesAsync();

            return Ok();
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
