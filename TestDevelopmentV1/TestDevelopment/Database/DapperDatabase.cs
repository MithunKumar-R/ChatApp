using Microsoft.Data.SqlClient;
using System.Data;

namespace TestDevelopment.Database
{
    public class DapperDatabase : IDapperConnection
    {
        private IDbConnection _connection;

        public IDbConnection Connection
        {
            get
            {
                _connection = new SqlConnection("Data Source=DESKTOP-FTK3CDE;Initial Catalog=Test;Integrated Security=true;TrustServerCertificate=true;");
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                return _connection;
            }
        }
    }
}
