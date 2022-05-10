using IncomeManager.Models;

namespace IncomeManager.Services
{
    public interface IIncomeServices
    {
        public Task<IEnumerable<Income>> GetIncome();
        public Task<Income> GetIncome(int id);
        public Task<Income> PutIncome(int id, Income income);
        public Task<Income> PostIncome(Income income);
        public Task DeleteIncome(int id);
    }
}
