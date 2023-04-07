using Amazon.QLDB.Driver;
using Amazon.QLDBSession;
using Amazon.IonDotnet.Builders;
using Amazon.Runtime;
using Amazon.QLDB.Driver.Serialization;

namespace OrganizationService.Utils
{
    public class QldbContext : IQldbContext
    {
        private AmazonQLDBSessionConfig config;
        private BasicAWSCredentials credentials;
        private ISerializer serializer = new ObjectSerializer();
        private Amazon.RegionEndpoint regionEndpoint = Amazon.RegionEndpoint.EUCentral1;
        private readonly string accessKey = "AKIAXUI34ORHSJFR6Q6Q";
        private readonly string secretKey = "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";
        private readonly string ledgerName = "supply-shield";
        public QldbContext()
        {
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
