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

        public Task<string> BuscarRotaMaisBarata(string origem, string destino)
        {
            // 1. Obter todas as rotas do repositório
            var rotas = _rotaRepository.GetAll().Result;

            // 2. Construir o grafo
            var grafo = new Dictionary<string, Dictionary<string, decimal>>();
            foreach (var rota in rotas)
            {
                if (!grafo.ContainsKey(rota.Origem))
                {
                    grafo[rota.Origem] = new Dictionary<string, decimal>();
                }
                grafo[rota.Origem][rota.Destino] = rota.Valor;
            }

            // 3. Implementar o Algoritmo de Dijkstra
            var distancias = new Dictionary<string, decimal>();
            var noAnterior = new Dictionary<string, string>();
            var noNaoVisitado = new HashSet<string>();

            // Inicializar distâncias
            foreach (var rota in rotas)
            {
                if (!distancias.ContainsKey(rota.Origem)) distancias[rota.Origem] = decimal.MaxValue;
                if (!distancias.ContainsKey(rota.Destino)) distancias[rota.Destino] = decimal.MaxValue;
                noNaoVisitado.Add(rota.Origem);
                noNaoVisitado.Add(rota.Destino);
            }

            distancias[origem] = 0;

            while (noNaoVisitado.Any())
            {
                var noAtual = noNaoVisitado
                    .Where(node => distancias.ContainsKey(node))
                    .OrderBy(node => distancias[node])
                    .FirstOrDefault();

                if (noAtual == null || distancias[noAtual] == decimal.MaxValue) break; // Não há mais caminhos acessíveis

                noNaoVisitado.Remove(noAtual);

                if (grafo.ContainsKey(noAtual))
                {
                    foreach (var vizinho in grafo[noAtual])
                    {
                        var novaDistancia = distancias[noAtual] + vizinho.Value;
                        if (novaDistancia < distancias[vizinho.Key])
                        {
                            distancias[vizinho.Key] = novaDistancia;
                            noAnterior[vizinho.Key] = noAtual;
                        }
                    }
                }
            }

            // 4. Reconstruir o caminho
            if (!distancias.ContainsKey(destino) || distancias[destino] == decimal.MaxValue)
            {
                return Task.FromResult($"Rota de {origem} para {destino} não encontrada.");
            }

            var caminho = new List<string>();
            var atual = destino;
            while (atual != null && noAnterior.ContainsKey(atual))
            {
                caminho.Insert(0, atual);
                atual = noAnterior[atual];
            }
            caminho.Insert(0, origem); // Adiciona a origem

            var rotaFormatada = string.Join(" - ", caminho);

            return Task.FromResult($"{rotaFormatada} ao custo de ${distancias[destino]}");
        }
    }
}
