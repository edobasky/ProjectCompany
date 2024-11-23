using Shared.DataTransferObjects;

namespace Service.Contract
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
        CompanyDto GetCompany(Guid companyId, bool trackChanges);

        CompanyDto CreateCompany(CompanyCreateDto company);
    }
}
