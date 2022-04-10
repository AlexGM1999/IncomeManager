﻿#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncomeManager.Data;
using IncomeManager.Models;

namespace IncomeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly IncomeManagerContext _context;

        public SalaryController(IncomeManagerContext context)
        {
            _context = context;
        }

        // GET: api/Salary
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salary>>> GetSalary()
        {
            return await _context.Salary.ToListAsync();
        }

        // GET: api/Salary/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Salary>> GetSalary(int id)
        {
            var salary = await _context.Salary.FindAsync(id);

            if (salary == null)
            {
                return NotFound();
            }

            return salary;
        }

        // PUT: api/Salary/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalary(int id, Salary salary)
        {
            if (id != salary.Id)
            {
                return BadRequest();
            }

            _context.Entry(salary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryExists(id))
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

        // POST: api/Salary
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Salary>> PostSalary(Salary salary)
        {
            _context.Salary.Add(salary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalary", new { id = salary.Id }, salary);
        }

        // DELETE: api/Salary/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalary(int id)
        {
            var salary = await _context.Salary.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }

            _context.Salary.Remove(salary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalaryExists(int id)
        {
            return _context.Salary.Any(e => e.Id == id);
        }
    }
}
