using System.ComponentModel.DataAnnotations;

namespace RotaDeViagem.API.ViewModels
{
    public class RotaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public required string Origem { get; set; }

        public required string Destino { get; set; }

        public decimal Valor { get; set; }
    }
}
