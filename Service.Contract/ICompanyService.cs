using Shared.DataTransferObjects;

namespace Service.Contract
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
    }
}
