using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Domain.Interfaces.Repositories;
using RotaDeViagem.Domain.Interfaces.Services;

namespace RotaDeViagem.Domain.Services
{
    public class RotaService : ServiceBase<Rota>, IRotaService
    {
        private readonly IRotaRepository _rotaRepository;
        public RotaService(IRotaRepository repository) : base(repository)
        {
            _rotaRepository = repository;
        }
    }
}
