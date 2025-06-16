using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Domain.Interfaces.Repositories;
using RotaDeViagem.Domain.Interfaces.Services;
using System.Runtime.CompilerServices;

namespace RotaDeViagem.Domain.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : Entity
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }
        public async Task Add(TEntity obj)
        {
             await _repository.Add(obj);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<TEntity?> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task Remove(Guid id)
        {
            await _repository.Remove(id);
        }

        public async Task Update(TEntity obj)
        {
            await _repository.Update(obj);
        }
    }

}
