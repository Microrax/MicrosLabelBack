using Microsoft.Data.SqlClient;
using System.Data;

namespace MicrosLabel.Infrastructure.Sql
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            var conn = new SqlConnection(_connectionString);

            conn.Open();

            return conn;
        }
    }
}