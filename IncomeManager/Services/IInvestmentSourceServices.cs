using IncomeManager.Models;

namespace IncomeManager.Services
{
    public interface IInvestmentSourceServices
    {
        public Task<IEnumerable<InvestmentSource>> GetInvestmentSources();
        public Task<InvestmentSource> GetInvestmentSource(int id);
        public Task<InvestmentSource> PutInvestmentSource(InvestmentSource investmentSource);
        public Task<InvestmentSource> PostInvestmentSource(InvestmentSource investmentSource);
        public Task DeleteInvestmentSource(int id);
    }
}
