using Google.Api.Gax.ResourceNames;
using Google.Cloud.SecretManager.V1;

namespace Gateway.Utils
{
    public class GoogleSecretManager : IGoogleSecretManager
    {
        private readonly string projectId = "506661741186";
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
