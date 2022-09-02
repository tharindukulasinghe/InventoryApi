using InventoryApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private readonly DataContext _context;
        public RepairController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Repair>> Get(int id)
        {
            var entity = await _context.Repairs
                .Include(repair => repair.Item)
                .FirstOrDefaultAsync(repair => repair.Id == id);

            return entity;
        }

        [HttpGet]
        public async Task<ActionResult<List<Repair>>> Get()
        {
            var entites = await _context.Repairs.Include(repair => repair.Item).ToListAsync();

            return entites;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Repair>> Update(int id, Repair request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return request;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<List<Summary>>> GetSummary()
        {
            var items = await _context.Summary.ToListAsync();

            return items;
        }
    }
}
