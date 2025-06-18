using RotaDeViagem.Domain.Entities;

namespace RotaDeViagem.Application.Interface
{
    public interface IRotaAppService : IAppServiceBase<Rota>
    {
        Task<string> BuscarRotaMaisBarata(string origem, string destino);
    }
}
