using Google.Api.Gax.ResourceNames;
using Google.Cloud.SecretManager.V1;

namespace OrganizationService.Utils
{
    public class GoogleSecretManager : IGoogleSecretManager
    {
        private readonly string projectId = "supply-shield-381721";
        public string KeyId { get; private set; } = "QLDBKey";
        public string AccessId { get; private set; } = "QLDBAccess";
        public Secret GetSecretMetadata(string secretId)
        {
            SecretManagerServiceClient client = SecretManagerServiceClient.Create();

            SecretName secretName = new SecretName(projectId, secretId);

            Secret secret = client.GetSecret(secretName);
            return secret;
        }
        public string GetSecret(string secretId, string secretVersionId)
        {
            SecretManagerServiceClient client = SecretManagerServiceClient.Create();
            AccessSecretVersionResponse result =  client.AccessSecretVersion(new SecretVersionName(projectId, secretId, secretVersionId));
            return result.Payload.Data.ToStringUtf8();
        }
    }
}
