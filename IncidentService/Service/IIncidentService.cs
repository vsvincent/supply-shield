using IncidentService.Models;

namespace IncidentService.Service
{
    public interface IIncidentService
    {
        public void Add(IIncident requestObject);
        public Task<IEnumerable<IIncident>> GetAll();
        public Task<IEnumerable<IIncident>> Get(string organizationId);
    }
}