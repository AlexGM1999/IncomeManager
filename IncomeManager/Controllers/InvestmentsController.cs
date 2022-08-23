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
        public async Task<Investment> PostInvestment(CreateInvestment investment)
        {
            return await _investmentServices.PostInvestment(investment).ConfigureAwait(false);
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
