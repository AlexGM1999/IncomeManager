#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncomeManager.Data;
using IncomeManager.Models;
using IncomeManager.Services;
using System.Data.Entity.Core;
using IncomeManager.DTOs;

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

        // GET: api/Investment/5
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

        // PUT: api/Investment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Investment>> PutInvestment(int id, Investment investment)
        {
            if (id != investment.Id)
            {
                return BadRequest();
            }
            try
            {
                return await _investmentServices.PutInvestment(id, investment).ConfigureAwait(false);
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task/*Task<ActionResult>*/ PostInvestment(CreateInvestment investment)
        {
            await _investmentServices.PostInvestment(investment).ConfigureAwait(false);
            //return CreatedAtAction("GetInvestment", new { id = investment.Id }, investment);
        }

        // DELETE: api/Investment/5
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
