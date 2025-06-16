using System.Linq.Expressions;

namespace RotaDeViagem.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        Task Add(TEntity obj);
        Task Update(TEntity obj);
        Task Remove(Guid id);
        Task<TEntity?> GetById(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
    }
}
