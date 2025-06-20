﻿namespace RotaDeViagem.Domain.Entities
{
    public class Rota : Entity
    {
        public required string Origem { get; set; }

        public required string Destino { get; set; }

        public decimal Valor { get; set; }
    }
}
