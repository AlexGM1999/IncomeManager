#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncomeManager.Services;
using IncomeManager.Models;
using System.Data.Entity.Core;

namespace IncomeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryServices _salaryServices;

        public SalaryController(ISalaryServices salaryServices)
        {
            _salaryServices = salaryServices;
        }

        // GET: api/Salary
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salary>>> GetSalary()
        {
            return new ActionResult<IEnumerable<Salary>>(await _salaryServices.GetSalary().ConfigureAwait(false));
        }

        // GET: api/Salary/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Salary>> GetSalary(int id)
        {
            try
            {
                return await _salaryServices.GetSalary(id).ConfigureAwait(false);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/Salary/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Salary>> PutSalary(int id, Salary salary)
        {
            if (id != salary.Id)
            {
               return BadRequest();
            }
            try
            {
               return await _salaryServices.PutSalary(id, salary).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (salary == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Salary
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostSalary(Salary salary)
        {
            await _salaryServices.PostSalary(salary).ConfigureAwait(false);
            return CreatedAtAction("GetSalary", new { id = salary.Id }, salary);
        }

        // DELETE: api/Salary/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSalary(int id)
        {
            try
            {
                await _salaryServices.DeleteSalary(id).ConfigureAwait(false);
                return NoContent();
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }        
        }
    }
}
