using IncomeManager.Models;

namespace IncomeManager.Services
{
    public interface IUserSevices
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetUser(int id);
    }
}
