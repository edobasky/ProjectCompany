using Entities.Models;

namespace Contract
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employees> GetEmployees(Guid companyId, bool trackChanges);
        Employees? GetEmployee(Guid companyId,Guid id, bool trackChanges);
    }
}
