using Amazon.IonDotnet.Tree.Impl;
using Amazon.IonDotnet.Tree;
using Amazon.QLDB.Driver;
using IncidentService.Models;

namespace IncidentService.Repository
{
    public interface IIncidentRepository
    {
        public void Add(IIncident incident);
        public Task<IEnumerable<IIncident>> GetAll();
    }
}
