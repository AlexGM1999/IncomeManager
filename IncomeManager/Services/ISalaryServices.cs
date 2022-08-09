using IncomeManager.Models;

namespace IncomeManager.Services
{
    public interface ISalaryServices
    {
        public Task<IEnumerable<Salary>> GetSalary(); 
        public Task<Salary> GetSalary(int id); 
        public Task<Salary> PutSalary(Salary salary); 
        public Task<Salary> PostSalary(Salary salary); 
        public Task DeleteSalary(int id);
       
    }
}
