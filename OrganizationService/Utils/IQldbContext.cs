using Amazon.QLDB.Driver;

namespace OrganizationService.Utils
{
    public interface IQldbContext
    {
        IQldbDriver GetDriver();
        IAsyncQldbDriver GetAsyncDriver();
    }
}
