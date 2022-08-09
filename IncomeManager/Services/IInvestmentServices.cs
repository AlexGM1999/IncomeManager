using IncomeManager.DTOs;
using IncomeManager.Models;

namespace IncomeManager.Services
{
    public interface IInvestmentServices
    {
        public Task<IEnumerable<Investment>> GetInvestments();
        public Task<Investment> GetInvestment(int id);
        public Task<Investment> PutInvestment(Investment investment);
        public Task<Investment> PostInvestment(CreateInvestment investment);
        public Task DeleteInvestment(int id);
    }
}
