using RotaDeViagem.Domain.Entities;

namespace RotaDeViagem.Domain.Interfaces.Services
{
    public interface IRotaService : IServiceBase<Rota>
    {
        Task<string> BuscarRotaMaisBarata(string origem, string destino);
    }
}
