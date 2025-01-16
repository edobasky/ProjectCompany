using Entities.Models;
using Shared.RequestFeatures;
using Shared.RequestFeatures.MetaData;

namespace Contract
{
    public interface IEmployeeRepository
    {
        Task<PagedList<Employees>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameter, bool trackChanges);
        Task<Employees?> GetEmployeeAsync(Guid companyId,Guid id, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employees employee);
        void DeleteEmployee(Employees employee);
    }
}
