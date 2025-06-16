using RotaDeViagem.Application.Interface;
using RotaDeViagem.Domain.Interfaces.Services;

namespace RotaDeViagem.Application.Services
{
    public class AppServiceBase<TEntity> : IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceBase;

        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public async Task Add(TEntity obj)
        {
            await _serviceBase.Add(obj);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _serviceBase.GetAll();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _serviceBase.GetById(id);
        }

        public async Task Remove(Guid id)
        {
            await _serviceBase.Remove(id);
        }

        public async Task Update(TEntity obj)
        {
            await _serviceBase.Update(obj);
        }
    }
}
