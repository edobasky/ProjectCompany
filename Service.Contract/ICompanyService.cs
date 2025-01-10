using Shared.DataTransferObjects;

namespace Service.Contract
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
        CompanyDto GetCompany(Guid companyId, bool trackChanges);
        CompanyDto CreateCompany(CompanyCreateDto company);
        IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyCreateDto> companyCollection);

        void DeleteCompany(Guid companyId, bool trackChanges);
        void UpdateCompany(Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges);
    }
}
