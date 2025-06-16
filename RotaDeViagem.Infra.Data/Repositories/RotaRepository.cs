using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Domain.Interfaces.Repositories;
using RotaDeViagem.Infra.Data.Context;

namespace RotaDeViagem.Infra.Data.Repositories
{
    public class RotaRepository : RepositoryBase<Rota>, IRotaRepository
    {
        public RotaRepository(RotaDeViagemDbContext context) : base(context)
        {
        }
    }
}
