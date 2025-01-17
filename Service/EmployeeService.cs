using AutoMapper;
using Contract;
using Entities.Exceptions;
using Entities.Models;
using Service.Contract;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using Shared.RequestFeatures.MetaData;
using System.ComponentModel.Design;

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
            await CheckIfCompanyExists(companyId, trackChanges);

            var employeeEntity = _mapper.Map<Employees>(employeeCreate);

            _repository.EmployeeRepository.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repository.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public async Task DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);

            var employeeForComapmny = await GetEmployeeForCompanyAndCheckIfItExists(companyId,id,trackChanges);

            _repository.EmployeeRepository.DeleteEmployee(employeeForComapmny);
            await _repository.SaveAsync();
        }

        public async Task<EmployeeDto> GetEmployee(Guid companyId, Guid id, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);

            var employeeDb = await _repository.EmployeeRepository.GetEmployeeAsync(companyId,id, trackChanges);

            if (employeeDb is null)
            {
                throw new EmployeeNotFoundException(id);
            }

            var employee = _mapper.Map<EmployeeDto>(employeeDb);

            return employee;
        }

        public async Task<(IEnumerable<EmployeeDto> employees, MetaData metaData)> GetEmployees(Guid companyId,EmployeeParameters employeeParameters, bool trackChanges)
        {
            if (!employeeParameters.ValidAgeRange) throw new MaxAgeRangeBadRequestException();

             await CheckIfCompanyExists(companyId,trackChanges);

            var employeesWithMetaData = await _repository.EmployeeRepository.GetEmployeesAsync(companyId,employeeParameters, trackChanges);

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);

            return (employees: employeesDto, metaData: employeesWithMetaData.metaData);
        }

        public async Task UpdateEmployeeForCompany(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges)
        {
            await CheckIfCompanyExists(companyId, compTrackChanges);

            var employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);

            _mapper.Map(employeeForUpdate, employeeEntity);
            await _repository.SaveAsync();
        } 

        private async Task CheckIfCompanyExists(Guid companyId,bool trackChanges)
        {
            var company = await _repository.CompanyRepository.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }
        }

        private async Task<Employees> GetEmployeeForCompanyAndCheckIfItExists(Guid companyId,Guid id, bool trackChanges)
        {
            var employeeDb = await _repository.EmployeeRepository.GetEmployeeAsync(companyId, id, trackChanges);
            if (employeeDb is null)
            {
                throw new EmployeeNotFoundException(id);
            }

            return employeeDb;
        }
    }
}
