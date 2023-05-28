using IncidentService.Models;
using IncidentService.Repository;

namespace IncidentService.Service
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;
        public IncidentService(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }
        public void Add(IIncident incident)
        {
            _incidentRepository.Add(incident);
        }

        public async Task<IEnumerable<IIncident>> GetAll()
        {
            return await _incidentRepository.GetAll();
        }
    }
}
