using OrganizationService.Models;

namespace OrganizationService.Services
{
    public interface IOrganizationService
    {
        void Add(IOrganization organization);
        Task<IEnumerable<IOrganization>> GetAll();
    }
}
