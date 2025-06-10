using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Foto
    {
        [Key]
        public long Id { get; set; }

        public long? VendedorId { get; set; }

        public string NomeArquivo { get; set; }

        public string TipoArquivo { get; set; }

        public byte[] ConteudoArquivo { get; set; }

        public DateTime DtInclusao { get; set; }

        public DateTime? DtExclusao { get; set; }

        public virtual Vendedor Vendedores { get; set; }
    }
}