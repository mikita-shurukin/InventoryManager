﻿using AutoMapper;
using InventoryManager.DAL;
using InventoryManager.DAL.Models;
using InventoryManager.Models.Requests.Item;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public ItemsController(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var item = await _context.Items.Include(i=>i.Group).AsNoTracking().ToListAsync();
            return View(item);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateItemRequest request)
        {

            var item = _mapper.Map<Item>(request);
            _context.Items.Add(item);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("Update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var updateItemRequest = new UpdateItemRequest
            {
                Name = item.Name,
                Price = item.Price
            };
            return View(updateItemRequest);
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateItemRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Name = request.Name;
            item.Price = request.Price;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Get));
        }
    }
}
