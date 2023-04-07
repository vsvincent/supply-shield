using Amazon.IonDotnet.Tree.Impl;
using Amazon.IonDotnet.Tree;
using Amazon.QLDB.Driver;
using OrganizationService.Models;

namespace OrganizationService.Repository
{
    public interface IOrganizationRepository
    {
        public void Add(IOrganization organization);
        public Task<IEnumerable<IOrganization>> GetAll();
    }
}
