using InventoryApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly DataContext _context;
        public ItemController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> Get()
        {
            var items = await _context.Items.ToListAsync();

            return items;
        }

        [HttpPost]
        public async Task<ActionResult<List<Item>>> Create(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return await Get();
        }
    }
}
