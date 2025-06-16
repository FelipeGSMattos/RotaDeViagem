using Microsoft.EntityFrameworkCore;
using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Domain.Interfaces.Repositories;
using RotaDeViagem.Infra.Data.Context;
using System.Linq.Expressions;

namespace RotaDeViagem.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : Entity
    {
        protected readonly RotaDeViagemDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected RepositoryBase(RotaDeViagemDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity?> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remove(Guid id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null)
                throw new InvalidOperationException($"Entidade do tipo {typeof(TEntity).Name} com ID {id} não encontrada.");

            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
