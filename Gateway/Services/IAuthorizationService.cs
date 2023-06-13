using Gateway.Models;

namespace Gateway.Services
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizeRole(FirebaseUser user, string requiredRole);
        Task<bool> AuthorizeOrganization(FirebaseUser user, string requiredOrganizationId);
    }
}
