using RotaDeViagem.Application.Interface;
using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Domain.Interfaces.Services;

namespace RotaDeViagem.Application.Services
{
    public class RotaAppService : AppServiceBase<Rota>, IRotaAppService
    {
        private readonly IRotaService _rotaService;
        public RotaAppService(IRotaService rotaService) : base(rotaService)
        {
            _rotaService = rotaService;
        }

        public async Task<string> BuscarMelhorRota(string origem, string destino)
        {
             return await _rotaService.BuscarMelhorRota(origem, destino);
        }
    }
}
