using Google.Api.Gax.ResourceNames;
using Google.Cloud.Firestore;
using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string project = "supply-shield-381721";
        private readonly FirestoreDb db;
        public UserRepository()
        {
            FirestoreDb db = FirestoreDb.Create(project);
        }
        public async Task<IUser> SetUser(IUser user)
        {
            DocumentReference docRef = db.Collection("users").Document(user.Email);
            Dictionary<string, object> firestoreUser = new Dictionary<string, object>
{
                { "EmployeeId", user.EmployeeId },
                { "OrganizationId", user.OrganizationId },
                { "Role", user.Role }
            };
            await docRef.SetAsync(user);
            return user;
        }
    }
}
