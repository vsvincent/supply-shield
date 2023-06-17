using Google.Api.Gax.ResourceNames;
using Google.Cloud.Firestore;
using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly static string project = "supply-shield-381721";
        private readonly FirestoreDb db = FirestoreDb.Create(project);
        private readonly ILogger<IUserRepository> _logger;
        public UserRepository(ILogger<IUserRepository> logger)
        {
            _logger = logger;
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
        public async Task<IUser> GetUserByEmail(string email)
        {
            DocumentReference docRef = db.Collection("users").Document(email);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                Dictionary<string, object> userDictionary = snapshot.ToDictionary();
                IUser user = new User
                {
                    Email = email,
                    EmployeeId = userDictionary["EmployeeId"].ToString(),
                    OrganizationId = userDictionary["OrganizationId"].ToString(),
                    Role = userDictionary["Role"].ToString()
                };
                return user;
            }
            else
            {
               _logger.LogInformation("User {0} does not exist!", snapshot.Id);
                return null;
            }
        }
    }
}
