namespace UserService.Models
{
    public class User : IUser
    {
        public string Email { get; set; }
        public string EmployeeId { get; set; }
        public string OrganizationId { get; set; }
        public string Role { get; set; }
    }
}
