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

namespace IncomeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentSourceController : ControllerBase
    {
        private readonly IInvestmentSourceServices _investmentSourceServices;

        public InvestmentSourceController(IInvestmentSourceServices investmentSourceServices)
        {
            _investmentSourceServices = investmentSourceServices;
        }

        // GET: api/InvestmentSources
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvestmentSource>>> GetInvestmentSources()
        {
            return new ActionResult<IEnumerable<InvestmentSource>>(await _investmentSourceServices.GetInvestmentSources());
        }

        // GET: api/InvestmentSources/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvestmentSource>> GetInvestmentSource(int id)
        {
            try
            {
                return await _investmentSourceServices.GetInvestmentSource(id);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/InvestmentSources/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<InvestmentSource>> PutInvestmentSource(int id, InvestmentSource investmentSource)
        {
            if (id != investmentSource.Id)
            {
                return BadRequest();
            }
            try
            {
                return await _investmentSourceServices.PutInvestmentSource(id, investmentSource);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (investmentSource == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/InvestmentSources
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InvestmentSource>> PostInvestmentSource(InvestmentSource investmentSource)
        {
            await _investmentSourceServices.PostInvestmentSource(investmentSource);
            return CreatedAtAction("GetInvestmentSource", new { id = investmentSource.Id }, investmentSource);
        }

        // DELETE: api/InvestmentSources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestmentSource(int id)
        {
            try
            {
                await _investmentSourceServices.DeleteInvestmentSource(id);
                return NoContent();
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
