using Google.Api.Gax.ResourceNames;
using Google.Cloud.SecretManager.V1;

namespace OrganizationService.Utils
{
    public class GoogleSecretManager : IGoogleSecretManager
    {
        private readonly string projectId = "506661741186";
        public string KeyId { get; private set; } = "QLDBKey";
        public string AccessId { get; private set; } = "QLDBAccess";
        public Secret GetSecretMetadata(string secretId)
        {
            // Create the client.
            SecretManagerServiceClient client = SecretManagerServiceClient.Create();

            // Build the resource name.
            SecretName secretName = new SecretName(projectId, secretId);

            // Call the API.
            Secret secret = client.GetSecret(secretName);
            return secret;
        }
        public string GetSecret(string secretId, string secretVersionId)
        {
            // Create the client.
            SecretManagerServiceClient client = SecretManagerServiceClient.Create();
            // Call the API.
            AccessSecretVersionResponse result =  client.AccessSecretVersion(new SecretVersionName(projectId, secretId, secretVersionId));
            return result.Payload.Data.ToStringUtf8();
        }
    }
}
