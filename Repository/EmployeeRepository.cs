using Contract;
using Entities.Models;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employees>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
