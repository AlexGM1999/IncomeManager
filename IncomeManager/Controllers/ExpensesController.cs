#nullable disable
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
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesServices _expensesServices;

        public ExpensesController(IExpensesServices expensesServices)
        {
            _expensesServices = expensesServices;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return new ActionResult<IEnumerable<Expense>>(await _expensesServices.GetExpenses().ConfigureAwait(false));
        }

        // GET: api/Expenses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            try
            {
                return  await _expensesServices.GetExpense(id).ConfigureAwait(false);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }      
        }

        // PUT: api/Expenses/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Expense>> PutExpense(Expense expence)
        {
            try
            {
                return await _expensesServices.PutExpense(expence).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (expence == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Expenses
        [HttpPost]
        public async Task<ActionResult> PostExpense(Expense expense)
        {
            if (expense == null)
            {
                return BadRequest();
            }

            await _expensesServices.PostExpense(expense);
            return CreatedAtAction("PostExpense", new { id = expense.Id }, expense);
        }

        // DELETE: api/Expenses/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpense(int id)
        {
            try
            {
                await _expensesServices.DeleteExpense(id).ConfigureAwait(false);
                return NoContent();
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }   
        }
    }
}
