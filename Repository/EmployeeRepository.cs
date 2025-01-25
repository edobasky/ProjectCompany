using Contract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using Shared.RequestFeatures.MetaData;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employees>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEmployeeForCompany(Guid companyId, Employees employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employees employee) => Delete(employee);

        public async Task<Employees?> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges) =>
            await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
        .SingleOrDefaultAsync();

        /* public async Task<IEnumerable<Employees> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameter, bool trackChanges) =>
             await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
             .OrderBy(e => e.Name)
             .Skip((employeeParameter.PageNumber - 1) * employeeParameter.PageSize)
             .Take(employeeParameter.PageSize)
             .ToListAsync();*/

        public async Task<PagedList<Employees>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameter, bool trackChanges)
        {
            var query = FindByCondition(e => e.CompanyId.Equals(companyId) && (e.Age >= employeeParameter.MinAge && e.Age <= employeeParameter.MaxAge), trackChanges);

            var employees = await query
           .OrderBy(e => e.Name)
           .FilterEmployees(employeeParameter.MinAge, employeeParameter.MaxAge)
           .Search(employeeParameter.SearchTerm)
           .Sort(employeeParameter.OrderBy)
             .Skip((employeeParameter.PageNumber - 1) * employeeParameter.PageSize)
             .Take(employeeParameter.PageSize)
           .ToListAsync();

            var count = await query.CountAsync();

            return PagedList<Employees>
                .ToPagedList(employees, count, employeeParameter.PageNumber, employeeParameter.PageSize);
        }

    }
}
