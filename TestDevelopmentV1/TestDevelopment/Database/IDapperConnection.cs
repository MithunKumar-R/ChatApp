using System.Data;

namespace TestDevelopment.Database
{
    public interface IDapperConnection
    {
        IDbConnection Connection { get; }

    }
}
