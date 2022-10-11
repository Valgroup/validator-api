using Microsoft.Data.SqlClient;

namespace Validator.API.Services
{
    public class SqlTcp
    {
        public static SqlConnectionStringBuilder NewSQLTCPConnectionString()
        {
            var connectionString = new SqlConnectionStringBuilder()
            {
                DataSource = "172.19.240.3",   // e.g. '127.0.0.1'
                // Set Host to 'cloudsql' when deploying to App Engine Flexible environment
                UserID = "avaliador",   // e.g. 'my-db-user'
                Password = "87a92d20bed44b789bed265350bec948", // e.g. 'my-db-password'
                InitialCatalog = "Avaliador-PRD", // e.g. 'my-database'
               
                Encrypt = false

            };
            connectionString.Pooling = true;
            // Specify additional properties here.
            return connectionString;
        }
    }
}
