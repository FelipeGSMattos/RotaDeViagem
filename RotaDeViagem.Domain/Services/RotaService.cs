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
            var valores = new Dictionary<string, decimal>();
            var noAnterior = new Dictionary<string, string>();
            var noNaoVisitado = new HashSet<string>();

            // Inicializar valores
            foreach (var rota in rotas)
            {
                if (!valores.ContainsKey(rota.Origem)) valores[rota.Origem] = decimal.MaxValue;
                if (!valores.ContainsKey(rota.Destino)) valores[rota.Destino] = decimal.MaxValue;
                noNaoVisitado.Add(rota.Origem);
                noNaoVisitado.Add(rota.Destino);
            }

            valores[origem] = 0;

            while (noNaoVisitado.Any())
            {
                var noAtual = noNaoVisitado
                    .Where(node => valores.ContainsKey(node))
                    .OrderBy(node => valores[node])
                    .FirstOrDefault();

                if (noAtual == null || valores[noAtual] == decimal.MaxValue) break; // Não há mais caminhos acessíveis

                noNaoVisitado.Remove(noAtual);

                if (grafo.ContainsKey(noAtual))
                {
                    foreach (var vizinho in grafo[noAtual])
                    {
                        var novaDistancia = valores[noAtual] + vizinho.Value;
                        if (novaDistancia < valores[vizinho.Key])
                        {
                            valores[vizinho.Key] = novaDistancia;
                            noAnterior[vizinho.Key] = noAtual;
                        }
                    }
                }
            }

            // 4. Reconstruir o caminho
            if (!valores.ContainsKey(destino) || valores[destino] == decimal.MaxValue)
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

            return Task.FromResult($"{rotaFormatada} ao custo de ${valores[destino]}");
        }
    }
}
