using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Domain.Interfaces.Repositories;
using RotaDeViagem.Domain.Interfaces.Services;

namespace RotaDeViagem.Domain.Services
{
    public class RotaService : ServiceBase<Rota>, IRotaService
    {
        private readonly IRotaRepository _rotaRepository;
        private Dictionary<string, List<(string destino, decimal custo)>> _grafo = new();
        private List<string> _melhorCaminho = new();
        private decimal _menorCusto;
        public RotaService(IRotaRepository repository) : base(repository)
        {
            _rotaRepository = repository;
        }

        public Task<string> BuscarMelhorRota(string origem, string destino)
        {
            origem = origem.ToUpper();
            destino = destino.ToUpper();

            var rotas = _rotaRepository.GetAll().Result;
            _grafo = rotas.GroupBy(r => r.Origem)
                .ToDictionary(g => g.Key, g => g.Select(r => (r.Destino, r.Valor)).ToList());


            var melhorCaminho = new List<string>();

            DFS(origem, destino, new List<string>(), 0);

            if (!_melhorCaminho.Any())
                return Task.FromResult($"Rota de {origem} para {destino} não encontrada.");

            var rotaFormatada = string.Join(" - ", _melhorCaminho);
            return Task.FromResult($"{rotaFormatada} ao custo de ${_menorCusto}");
        }

        private void DFS(string atual, string destinoFinal, List<string> caminhoAtual, decimal custoTotal)
        {
            if (custoTotal >= _menorCusto)
                return;

            caminhoAtual.Add(atual);

            if (atual == destinoFinal)
            {
                _menorCusto = custoTotal;
                _melhorCaminho = new List<string>(caminhoAtual);
                caminhoAtual.RemoveAt(caminhoAtual.Count - 1); // backtrack
                return;
            }

            if (!_grafo.ContainsKey(atual))
            {
                caminhoAtual.RemoveAt(caminhoAtual.Count - 1); // backtrack
                return;
            }

            foreach (var (vizinho, custo) in _grafo[atual])
            {
                if (!caminhoAtual.Contains(vizinho))
                {
                    DFS(vizinho, destinoFinal, caminhoAtual, custoTotal + custo);
                }
            }

            caminhoAtual.RemoveAt(caminhoAtual.Count - 1); // backtrack
        }
    }
}
