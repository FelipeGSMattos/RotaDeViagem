namespace RotaDeViagem.Application.Interface
{
    public interface IAppServiceBase<TEntity> where TEntity : class
    {
        Task Add(TEntity obj);
        Task Update(TEntity obj);
        Task Remove(Guid id);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
