using Google.Cloud.SecretManager.V1;

namespace IncidentService.Utils
{
    public interface IGoogleSecretManager
    {
        string KeyId { get;}
        string AccessId { get;}
        string GetSecret(string secretId, string secretVersionId);
        Secret GetSecretMetadata(string secretId);
    }
}
