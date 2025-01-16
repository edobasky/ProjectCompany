using Entities.Models;
using Shared.RequestFeatures;

namespace Contract
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employees>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameter, bool trackChanges);
        Task<Employees?> GetEmployeeAsync(Guid companyId,Guid id, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employees employee);
        void DeleteEmployee(Employees employee);
    }
}
