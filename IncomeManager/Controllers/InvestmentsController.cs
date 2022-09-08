#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncomeManager.Models;
using IncomeManager.Services;
using System.Data.Entity.Core;

namespace IncomeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentsController : ControllerBase
    {
        private readonly IInvestmentServices _investmentServices;

        public InvestmentsController(IInvestmentServices investmentServices)
        {
            _investmentServices = investmentServices;
        }

        // GET: api/Investments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Investment>>> GetInvestments()
        {
            return new ActionResult<IEnumerable<Investment>>(await _investmentServices.GetInvestments().ConfigureAwait(false));
        }

        // GET: api/Investment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Investment>> GetInvestment(int id)
        {
            try
            {
                return await _investmentServices.GetInvestment(id).ConfigureAwait(false);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/Investment/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Investment>> PutInvestment(Investment investment)
        {
            try
            {
                return await _investmentServices.PutInvestment(investment).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (investment == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Investment
        [HttpPost]
        public async Task<ActionResult<Investment>> PostInvestment(Investment investment)
        {
            if (investment == null)
            {
                return BadRequest();
            }

            await _investmentServices.PostInvestment(investment).ConfigureAwait(false);
            return CreatedAtAction("PostInvestment", new { id = investment.Id }, investment);
        }

        // DELETE: api/Investment/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSalary(int id)
        {
            try
            {
                await _investmentServices.DeleteInvestment(id).ConfigureAwait(false);
                return NoContent();
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
