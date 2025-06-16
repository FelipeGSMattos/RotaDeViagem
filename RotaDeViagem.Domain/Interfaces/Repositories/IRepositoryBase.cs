using RotaDeViagem.Domain.Entities;
using System.Linq.Expressions;

namespace RotaDeViagem.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);
        Task<TEntity?> GetById(Guid id);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Remove(Guid id);
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
