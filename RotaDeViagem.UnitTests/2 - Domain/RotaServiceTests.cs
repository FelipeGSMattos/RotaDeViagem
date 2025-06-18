using Microsoft.EntityFrameworkCore;
using Moq;
using RotaDeViagem.Domain.Entities;
using RotaDeViagem.Domain.Interfaces.Repositories;
using RotaDeViagem.Domain.Services;
using RotaDeViagem.Infra.Data.Context;

namespace Tests
{
    public class RotaServiceTests
    {
        private IRotaRepository GetMockRepository()
        {
            var mock = new Mock<IRotaRepository>();

            var rotas = new List<Rota>
            {
                new Rota { Origem = "GRU", Destino = "BRC", Valor = 10 },
                new Rota { Origem = "BRC", Destino = "SCL", Valor = 5 },
                new Rota { Origem = "GRU", Destino = "CDG", Valor = 75 },
                new Rota { Origem = "GRU", Destino = "SCL", Valor = 20 },
                new Rota { Origem = "GRU", Destino = "ORL", Valor = 56 },
                new Rota { Origem = "ORL", Destino = "CDG", Valor = 5 },
                new Rota { Origem = "SCL", Destino = "ORL", Valor = 20 }
            };

            mock.Setup(r => r.GetAll()).ReturnsAsync(rotas);

            return mock.Object;
        }

        [Fact]
        public async Task BuscarRotaMaisBarata_DeveRetornarRotaMaisBarataComMultiplasConexoes()
        {
            // Arrange
            var repository = GetMockRepository();
            var service = new RotaService(repository);

            // Act
            var resultado = await service.BuscarRotaMaisBarata("GRU", "CDG");

            // Assert
            Assert.Equal("GRU - BRC - SCL - ORL - CDG ao custo de $40", resultado);
        }

        [Fact]
        public async Task BuscarRotaMaisBarata_DeveRetornarRotaDireta_SeForMaisBarata()
        {
            // Arrange
            var repository = GetMockRepository();
            var service = new RotaService(repository);

            // Act
            var resultado = await service.BuscarRotaMaisBarata("BRC", "SCL");

            // Assert
            Assert.Equal("BRC - SCL ao custo de $5", resultado);
        }

        [Fact]
        public async Task BuscarRotaMaisBarata_DeveRetornarMensagemSeNaoExistirRota()
        {
            // Arrange
            var repository = GetMockRepository();
            var service = new RotaService(repository);

            // Act
            var resultado = await service.BuscarRotaMaisBarata("SCL", "BRC");

            // Assert
            Assert.Equal("Rota de SCL para BRC não encontrada.", resultado);
        }
    }
}
