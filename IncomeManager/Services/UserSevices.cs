using IncomeManager.Data;
using IncomeManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core;

namespace IncomeManager.Services
{
    public class UserSevices : IUserSevices
    {
        private readonly IncomeManagerContext _context;

        public UserSevices(IncomeManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new ObjectNotFoundException();
            }

            return user;
        }
    }
}
