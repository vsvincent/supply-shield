namespace OrganizationService.Models
{
    public interface IOrganization
    {
        public string? Code { get; }
        public string? Name { get; set; }
        public ICollection<string>? AdministratorIds { get; set; }
        public ICollection<string>? ManagerIds { get; set; }
        public ICollection<string>? ObserverIds { get; set; }
    }
}
