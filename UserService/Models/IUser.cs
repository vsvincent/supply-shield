namespace UserService.Models
{
    public interface IUser
    {
        public string Email { get; set; }
        public string EmployeeId { get; set; }
        public string OrganizationId { get; set; }
        public string Role { get; set; }
    }
}
