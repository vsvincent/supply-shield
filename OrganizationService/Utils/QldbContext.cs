using Amazon.QLDB.Driver;
using Amazon.QLDBSession;
using Amazon.IonDotnet.Builders;
using Amazon.Runtime;
using Amazon.QLDB.Driver.Serialization;

namespace OrganizationService.Utils
{
    public class QldbContext : IQldbContext
    {
        IGoogleSecretManager _googleSecretManager;
        private AmazonQLDBSessionConfig config;
        private BasicAWSCredentials credentials;
        private ISerializer serializer = new ObjectSerializer();
        private Amazon.RegionEndpoint regionEndpoint = Amazon.RegionEndpoint.EUCentral1;
        private readonly string accessKey;
        private readonly string secretKey;
        private readonly string ledgerName = "supply-shield";
        public QldbContext(IGoogleSecretManager googleSecretManager)
        {
            _googleSecretManager = googleSecretManager;
            accessKey = _googleSecretManager.GetSecret(_googleSecretManager.AccessId, "1");
            secretKey = _googleSecretManager.GetSecret(_googleSecretManager.KeyId, "1");
            config = new AmazonQLDBSessionConfig();
            config.RegionEndpoint = regionEndpoint;
            credentials = new BasicAWSCredentials(accessKey, secretKey);
        }
        public IAsyncQldbDriver GetAsyncDriver()
        {
            return AsyncQldbDriver.Builder()
                .WithLedger(ledgerName)
                .WithSerializer(serializer)
                .WithAWSCredentials(credentials)
                .WithQLDBSessionConfig(config).Build();
        }

        public IQldbDriver GetDriver()
        {
            return QldbDriver.Builder()
                .WithLedger(ledgerName)
                .WithSerializer(serializer)
                .WithAWSCredentials(credentials)
                .WithQLDBSessionConfig(config).Build();
        }
    }
}
