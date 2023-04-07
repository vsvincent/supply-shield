using OrganizationService.Models;
using OrganizationService.Repository;

namespace OrganizationService.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        public OrganizationService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }
        public void Add(IOrganization organization)
        {
            _organizationRepository.Add(organization);
        }
        public Task<IEnumerable<IOrganization>> GetAll()
        {
            return _organizationRepository.GetAll();
        }
    }
}
