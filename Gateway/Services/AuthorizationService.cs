using Gateway.Models;
using Google.Cloud.Firestore;
using System.Collections.Generic;

namespace Gateway.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly string project = "supply-shield-381721";
        private readonly string userCollection = "users";
        private readonly FirestoreDb db;
        public AuthorizationService()
        {
            FirestoreDb db = FirestoreDb.Create(project);
        }
        public async Task<bool> AuthorizeRole(FirebaseUser user, string requiredRole)
        {
            DocumentReference docRef = db.Collection(userCollection).Document(user.Email);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            string role;
            if (snapshot.Exists)
            {
                Dictionary<string, object> userDocument = snapshot.ToDictionary();
                role = userDocument["Role"].ToString();
            }
            else
            {
                throw new Exception($"Document {snapshot.Id} does not exist!");
            }

            if (string.IsNullOrEmpty(role)) return false;
            //Admin always authorized
            if (role == "Administrator") return true;

            if (role == "Manager" && requiredRole != "Administrator") return true;

            if (role == "Viewer" && requiredRole == "Viewer") return true;

            return false;
        }
        public async Task<bool> AuthorizeOrganization(FirebaseUser user, string requiredOrganizationId)
        {
            DocumentReference docRef = db.Collection(userCollection).Document(user.Email);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            string organizationId;
            if (snapshot.Exists)
            {
                Dictionary<string, object> userDocument = snapshot.ToDictionary();
                organizationId = userDocument["OrganizationId"].ToString();
            }
            else
            {
                throw new Exception($"Document {snapshot.Id} does not exist!");
            }
            if (string.IsNullOrEmpty(organizationId)) return false;

            if(organizationId == requiredOrganizationId) return true;

            return false;
        }
    }
}
