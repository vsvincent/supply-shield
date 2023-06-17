using Google.Cloud.SecretManager.V1;

namespace UserService.Utils
{
    public interface IGoogleSecretManager
    {
        string GetSecret(string secretId, string secretVersionId);
        Secret GetSecretMetadata(string secretId);
    }
}
