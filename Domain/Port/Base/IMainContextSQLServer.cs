using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.Port
{
	public partial interface IMainContextSQLServer : IMainContext
	{
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
		int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
		int ExecuteQuery(string sQuery);
		Task<int> ExecuteQueryAsync(string sQuery);
	}
}

