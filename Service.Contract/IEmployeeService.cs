using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contract
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees(Guid companyId,EmployeeParameters employeeParameters, bool trackChanges);
        Task<EmployeeDto> GetEmployee(Guid companyId, Guid id, bool trackChanges);

        Task<EmployeeDto> CreateEmployeeForCompany(Guid companyId, EmployeeCreateDto employeeCreate, bool trackChanges);
        Task DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges);
        Task UpdateEmployeeForCompany(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges);
    }
}
