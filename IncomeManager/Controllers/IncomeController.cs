﻿#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncomeManager.Models;
using IncomeManager.Services;
using System.Data.Entity.Core;

namespace IncomeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeServices _incomeServices;

        public IncomeController(IIncomeServices incomeServices)
        {
            _incomeServices = incomeServices;
        }

        // GET: api/Income
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncome()
        {
            return new ActionResult<IEnumerable<Income>>(await _incomeServices.GetIncome().ConfigureAwait(false));
        }

        // GET: api/Income/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Income>> GetIncome(int id)
        {
            try
            {
                return await _incomeServices.GetIncome(id).ConfigureAwait(false);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/Income/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Income>> PutIncome(Income income)
        {
            try
            {
                return await _incomeServices.PutIncome(income).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (income == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Income
        [HttpPost]
        public async Task<ActionResult<Income>> PostIncome(Income income)
        {
            if (income == null)
            {
                return BadRequest();
            }

            await _incomeServices.PostIncome(income);
            return CreatedAtAction("PostIncome", new { id = income.Id }, income);
        }

        // DELETE: api/Income/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIncome(int id)
        {
            try
            {
                await _incomeServices.DeleteIncome(id).ConfigureAwait(false);
                return NoContent();
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
