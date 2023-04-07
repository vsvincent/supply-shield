using System.ComponentModel.DataAnnotations;

namespace OrganizationService.Models
{
    public class Organization : IOrganization
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public ICollection<string>? AdministratorIds { get; set; }
        public ICollection<string>? ManagerIds { get; set; }
        public ICollection<string>? ObserverIds { get; set; }
        public Organization()
        {
            
        }

        public Organization(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
