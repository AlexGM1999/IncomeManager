using IncomeManager.Models;
using IncomeManager.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Core;

namespace IncomeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserSevices _userServices;

        public UsersController(IUserSevices userServices)
        {
            _userServices = userServices;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return new ActionResult<IEnumerable<User>>(await _userServices.GetUsers().ConfigureAwait(false));
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetSalary(int id)
        {
            try
            {
                return await _userServices.GetUser(id).ConfigureAwait(false);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
        }
    }
}

