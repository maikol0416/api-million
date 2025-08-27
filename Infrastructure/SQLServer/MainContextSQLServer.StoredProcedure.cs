using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.SQLServer
{
	public partial class MainContextSQLServer
	{
		public int ExecuteQuery(string query)
		{
			return Database.ExecuteSqlRaw(query);
		}
		public int ExecuteQuery(string query, params SqlParameter[] sqlParameters)
		{
			return Database.ExecuteSqlRaw(query, sqlParameters);
		}
        public Task<int> ExecuteQueryAsync(string query)
        {
            return Database.ExecuteSqlRawAsync(query);
        }
        public Task<int> ExecuteQueryAsync(string query, params SqlParameter[] sqlParameters)
        {
            return Database.ExecuteSqlRawAsync(query, sqlParameters);
        }
    }
}

