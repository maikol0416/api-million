using Domain.Entities;
using Domain.Port;
using Infrastructure.Repository.Bases;

namespace Infrastructure.Repository
{
    public class TestRepository : RepositoryBaseSQLServer<Test>, IRepositoryBase<Test>, ITestRepository
    {
        public TestRepository(IMainContextSQLServer mainContext)
            : base(mainContext)
        {

        }
    }
}
