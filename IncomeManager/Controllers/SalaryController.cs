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

        // GET: api/Salary/{id}
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

        // PUT: api/Salary/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Salary>> PutSalary(Salary salary)
        {
            try
            {
               return await _salaryServices.PutSalary(salary).ConfigureAwait(false);
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
        [HttpPost]
        public async Task<ActionResult> PostSalary(Salary salary)
        {
            if (salary == null)
            {
                return BadRequest();
            }
            await _salaryServices.PostSalary(salary).ConfigureAwait(false);
            return CreatedAtAction("PostSalary", new { id = salary.Id }, salary);
        }

        // DELETE: api/Salary/{id}
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
