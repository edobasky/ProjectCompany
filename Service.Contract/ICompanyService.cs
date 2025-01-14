﻿using Shared.DataTransferObjects;

namespace Service.Contract
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompanies(bool trackChanges);
        Task<CompanyDto> GetCompany(Guid companyId, bool trackChanges);
        Task<CompanyDto> CreateCompany(CompanyCreateDto company);
        Task<IEnumerable<CompanyDto>> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollection(IEnumerable<CompanyCreateDto> companyCollection);

        Task DeleteCompany(Guid companyId, bool trackChanges);
        Task UpdateCompany(Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges);
    }
}
