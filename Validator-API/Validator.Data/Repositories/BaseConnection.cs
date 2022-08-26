using Microsoft.Data.SqlClient;
using System.Data;
using Validator.Domain.Core.Helpers;

namespace Validator.Data.Repositories
{
    public class BaseConnection : IDisposable
    {
        public IDbConnection CnRead
        {
            get
            {
                return new SqlConnection(RuntimeConfigurationHelper.ConnectionString);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
