using AutoMapper;
using Contract;
using Entities.Exceptions;
using Entities.Models;
using Service.Contract;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeCreateDto employeeCreate, bool trackChanges)
        {
            var company = _repository.CompanyRepository.GetCompany(companyId, trackChanges);

            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employeeEntity = _mapper.Map<Employees>(employeeCreate);

            _repository.EmployeeRepository.CreateEmployeeForCompany(companyId, employeeEntity);
            _repository.Save();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public void DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges)
        {
            var company = _repository.CompanyRepository.GetCompany(companyId,trackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employeeForComapmny = _repository.EmployeeRepository.GetEmployee(companyId, id,trackChanges);
            if(employeeForComapmny is null)
            {
                throw new EmployeeNotFoundException(id);
            }

            _repository.EmployeeRepository.DeleteEmployee(employeeForComapmny);
            _repository.Save();
        }

        public EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges)
        {
            var company = _repository.CompanyRepository.GetCompany(companyId, trackChanges);

            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employeeDb = _repository.EmployeeRepository.GetEmployee(companyId,id, trackChanges);

            if (employeeDb is null)
            {
                throw new EmployeeNotFoundException(id);
            }

            var employee = _mapper.Map<EmployeeDto>(employeeDb);

            return employee;
        }

        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = _repository.CompanyRepository.GetCompany(companyId, trackChanges);

            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employeesFromDb = _repository.EmployeeRepository.GetEmployees(companyId, trackChanges);

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);

            return employeesDto;
        }
    }
}
