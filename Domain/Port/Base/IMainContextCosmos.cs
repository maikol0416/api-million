using MongoDB.Driver;

namespace Domain.Port
{
    public interface IMainContextCosmos : IMainContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string name);
    }
}
