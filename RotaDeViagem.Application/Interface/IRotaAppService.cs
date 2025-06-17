using RotaDeViagem.Domain.Entities;

namespace RotaDeViagem.Application.Interface
{
    public interface IRotaAppService : IAppServiceBase<Rota>
    {
        Task<string> BuscarMelhorRota(string origem, string destino);
    }
}
