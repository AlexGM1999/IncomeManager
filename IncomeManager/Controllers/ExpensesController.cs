#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncomeManager.Data;
using IncomeManager.Models;

namespace IncomeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IncomeManagerContext _context;

        public ExpensesController(IncomeManagerContext context)
        {
            _context = context;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expenses>>> GetExpence()
        {
            return await _context.Expenses.ToListAsync();
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expenses>> GetExpence(int id)
        {
            var expence = await _context.Expenses.FindAsync(id);

            if (expence == null)
            {
                return NotFound();
            }

            return expence;
        }

        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpence(int id, Expenses expence)
        {
            if (id != expence.Id)
            {
                return BadRequest();
            }

            _context.Entry(expence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenceExists(id))
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

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expenses>> PostExpence(Expenses expence)
        {
            _context.Expenses.Add(expence);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpence", new { id = expence.Id }, expence);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpence(int id)
        {
            var expence = await _context.Expenses.FindAsync(id);
            if (expence == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expence);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenceExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
