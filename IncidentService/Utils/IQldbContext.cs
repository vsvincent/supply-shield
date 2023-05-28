using Amazon.QLDB.Driver;

namespace IncidentService.Utils
{
    public interface IQldbContext
    {
        IQldbDriver GetDriver();
        IAsyncQldbDriver GetAsyncDriver();
    }
}
