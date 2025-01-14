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

        public async Task<EmployeeDto> CreateEmployeeForCompany(Guid companyId, EmployeeCreateDto employeeCreate, bool trackChanges)
        {
            var company = await _repository.CompanyRepository.GetCompanyAsync(companyId, trackChanges);

            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employeeEntity = _mapper.Map<Employees>(employeeCreate);

            _repository.EmployeeRepository.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repository.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public async Task DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges)
        {
            var company = await _repository.CompanyRepository.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employeeForComapmny = await _repository.EmployeeRepository.GetEmployeeAsync(companyId, id,trackChanges);
            if(employeeForComapmny is null)
            {
                throw new EmployeeNotFoundException(id);
            }

            _repository.EmployeeRepository.DeleteEmployee(employeeForComapmny);
            await _repository.SaveAsync();
        }

        public async Task<EmployeeDto> GetEmployee(Guid companyId, Guid id, bool trackChanges)
        {
            var company = await _repository.CompanyRepository.GetCompanyAsync(companyId, trackChanges);

            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employeeDb = await _repository.EmployeeRepository.GetEmployeeAsync(companyId,id, trackChanges);

            if (employeeDb is null)
            {
                throw new EmployeeNotFoundException(id);
            }

            var employee = _mapper.Map<EmployeeDto>(employeeDb);

            return employee;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = await _repository.CompanyRepository.GetCompanyAsync(companyId, trackChanges);

            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employeesFromDb = await _repository.EmployeeRepository.GetEmployeesAsync(companyId, trackChanges);

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);

            return employeesDto;
        }

        public async Task UpdateEmployeeForCompany(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges)
        {
            var company = await _repository.CompanyRepository.GetCompanyAsync(companyId, compTrackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employeeEntity = await _repository.EmployeeRepository.GetEmployeeAsync(companyId, id, empTrackChanges);
            if (employeeEntity is null)
            {
                throw new EmployeeNotFoundException(id);
            }

            _mapper.Map(employeeForUpdate, employeeEntity);
            await _repository.SaveAsync();
        }
    }
}
