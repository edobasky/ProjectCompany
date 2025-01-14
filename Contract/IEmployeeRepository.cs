using Entities.Models;

namespace Contract
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employees>> GetEmployeesAsync(Guid companyId, bool trackChanges);
        Task<Employees?> GetEmployeeAsync(Guid companyId,Guid id, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employees employee);
        void DeleteEmployee(Employees employee);
    }
}
