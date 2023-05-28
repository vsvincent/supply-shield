namespace IncidentService.Models
{
    public interface IIncident
    {
        public string? Id { get; set; }
        public string UserId { get; set; }
        public string OrganizationId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
